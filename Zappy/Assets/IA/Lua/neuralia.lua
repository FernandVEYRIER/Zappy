--
-- Created by IntelliJ IDEA.
-- User: gaspar_q
-- Date: 6/14/16
-- Time: 7:25 PM
-- To change this template use File | Settings | File Templates.
--

--[[
-- Inputs:
--      -   Le nombre d'objets qui sont devant moi et dont j'ai besoin pour mon élévation
--      -   Le nombre de nourriture que j'ai dans mon inventaire
--      -   Le nombre de nourriture qu'il y a devant moi
--      -   Le nombre de joueurs devant moi
--      -   Pourcentage d'objets necessaires pour l'incantation complete
--      -   SearchMode
--
-- Outputs:
--      -   Avance
--      -   Tourne à droite
--      -   Tourne à gauche
--      -   Se Nourrir
--      -   Broadcast pour incantation
--
--  todo modification sur les outputs au niveau de 'se nourrir'
--  todo ajouter des actions automatiques:
--      'prendre ressource necessaire'
--      'poser ressource pour incantation' (si nb players sur la case suffisant)
--      'incantation' si les conditions sont ok
 ]]

local path = debug.getinfo(1).source:match("@?(.*/)") or "";

local net = require(path.."neural-network/Network");
local queue = require(path.."Queue");

local neuralNet;
local netActions = {};
local actionQueue = queue.new();
local pendingActions = queue.new();
local doingAction = false;
local searching = 0.0;

function OnStart()
    netActions[1] = MOVE;
    netActions[2] = RIGHT;
    netActions[3] = LEFT;
    netActions[4] = TAKE;
    netActions[5] = DROP;
    netActions[6] = BROADCAST;
    local filename = io.read();
    if (filename == "") then
        neuralNet = net.new(6, 6, {2, 5, 2});
    else
        neuralNet = net.deserialize(filename);
    end
end

local function GetNbOfNeededRessources(fullSight)
    local nb = 0;

    for i=LINEMATE, THYSTAME do
        local localNb = fullSight:GetNbOf(i);

        if (localNb > 0 and IA:NeedRessources(i)) then
            nb = nb + localNb;
        end
    end
    return nb;
end

function CanTakeDropRessource(takeordrop)
    local sigthAtPos = IA:GetSightAt(0);

    for i=LINEMATE, THYSTAME do
        if (IA:NeedRessources(i) and takeordrop(sigthAtPos, i)) then
            return Inventory.GetNameOf(i);
        end
    end
    return nil;
end

function OnUpdate()
    local todo;
    local fullSight = IA:GetFullSight();
    local param;

    todo = queue.pop(actionQueue);
    if (todo == nil and doingAction == false) then
        net.compute(neuralNet, {
            GetNbOfNeededRessources(fullSight),
            IA:GetInventory():GetNbOf(FOOD),
            fullSight:GetNbOf(FOOD),
            fullSight:GetNbOf(PLAYER),
            1.0 - IA:ElevationPercentage(),
            searching
        });
        for i=1, #neuralNet.output.neurons do
            if (neuralNet.output.neurons[i].value > 0.75) then
                queue.push(actionQueue, netActions[i]);
            end
        end;
        todo = queue.pop(actionQueue);
    end
    if (todo == nil) then
        return NONE;
    end
    if (todo == BROADCAST) then
        param = "Incant "..IA:GetLevel();

    elseif (todo == TAKE) then
        param = CanTakeDropRessource(function (sightAtPos, ressource)
            return (sightAtPos:HasObject(ressource));
        end);
    elseif (todo == DROP) then
        param = CanTakeDropRessource(function (_, ressource)
            return (IA:GetInventory():GetNbOf(ressource));
        end);
    end
    if (param ~= nil) then
        IA:SetParameter(param);
    end
    doingAction = true;
    queue.push(pendingActions, todo);
    return todo;
end

function OnReceive(reqCode, answer)
    if (reqCode == pendingActions[pendingActions.last]) then
        if (reqCode == BROADCAST) then
            if (answer == "ok") then
                doingAction = false;
            end
        elseif (reqCode == INCANTATION) then
            if (answer == "elevation en cours") then
                doingAction = false;
            end
        else
            doingAction = false;
        end
    else
        if (reqCode == BROADCAST) then
            local lvl = answer:match("Incant (%d+)");

            if (lvl ~= nil and tonumber(lvl) == IA:GetLevel()) then
                searching = tonumber(answer:match("message (%d)"));
            end
        end
    end
    if (doingAction == false) then
        queue.pop(pendingActions);
    end
end