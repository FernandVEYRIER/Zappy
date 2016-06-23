--
-- Created by IntelliJ IDEA.
-- User: gouet_v and wery_a
-- Date: 6/8/16
-- Time: 12:50 PM
-- To change this template use File | Settings | File Templates.
--

local path = "src_client_ai/lua/"

canAct = true;
Queue = require (path.."Queue")

dataQueue = {}
serverAnswer = {}
local objectOnMap = {}

toto = 0


function OnStart()
   dataQueue = Queue.new()

   objectOnMap[FOOD] = "nourriture"
   objectOnMap[LINEMATE] = "linemate"
   objectOnMap[DERAUMERE] = "deraumere"
   objectOnMap[SIBUR] = "sibur"
   objectOnMap[MENDIANE] = "mendiane"
   objectOnMap[PHIRAS] = "phiras"
   objectOnMap[THYSTAME] = "thystame"

   serverAnswer[MOVE] = onCallMove
   serverAnswer[LEFT] = onCallMove
   serverAnswer[RIGHT] = onCallMove
   serverAnswer[TAKE] = onCallTake
   serverAnswer[DROP] = onCallDrop
   serverAnswer[LAYEGG] = nil
   serverAnswer[BROADCAST] = onBroadCast
end

function onBroadCast(requestCode, responseServer)
   if responseServer == "ok" then
      canAct = true
      return
   end
   local dir, level = responseServer:match("message ([0-9]), elevate ([0-9])")
   if dir == nil or level == nil then
      print("failed")
      return
   end
   print(dir, level)
   if IA:GetLevel() == tonumber(level) and IA:CanElevate() then
      if dir == "1" then
	 Queue.push(dataQueue, {MOVE, nil})
      elseif dir == "2" then
 	Queue.push(dataQueue, {MOVE, nil})
	 Queue.push(dataQueue, {LEFT, nil})
	 Queue.push(dataQueue, {MOVE, nil})
      elseif dir == "3" then
	 Queue.push(dataQueue, {LEFT, nil})
	 Queue.push(dataQueue, {MOVE, nil})
      elseif dir == "4" then
	 Queue.push(dataQueue, {LEFT, nil})
	 Queue.push(dataQueue, {MOVE, nil})
	 Queue.push(dataQueue, {LEFT, nil})
	 Queue.push(dataQueue, {MOVE, nil})
      elseif dir == "5" then
	 Queue.push(dataQueue, {LEFT, nil})
	 Queue.push(dataQueue, {LEFT, nil})
	 Queue.push(dataQueue, {MOVE, nil})
      elseif dir == "6" then
	 Queue.push(dataQueue, {RIGHT, nil})
	 Queue.push(dataQueue, {MOVE, nil})
	 Queue.push(dataQueue, {RIGHT, nil})
	 Queue.push(dataQueue, {MOVE, nil})
      elseif dir == "7" then
	 Queue.push(dataQueue, {RIGHT, nil})
	 Queue.push(dataQueue, {MOVE, nil})
      elseif dir == "8" then
	 Queue.push(dataQueue, {MOVE, nil})
	 Queue.push(dataQueue, {RIGHT, nil})
	 Queue.push(dataQueue, {MOVE, nil})
      end
   end
end

function onCallDrop(requestCode, responseServer)
   canAct = true
end

function onCallTake(requestCode, responseServer)
   canAct = true
end

function onCallMove(requestCode, responseServer)
   canAct = true
end

function needRessource(ressource)
   if (IA:GetNbNeededRessources(ressource) > IA:GetInventory():GetNbOf(ressource)) then
      return (true)
   end
   return (false)
end

function elevateDarracq()
   for ressources, ressourcesString in ipairs(objectOnMap) do
      local i = 0
      while i < IA:GetNbNeededRessources(ressources) do
	 if IA:GetInventory():GetNbOf(ressources) == 0 then
	    return false
	 end
	 Queue.push(dataQueue, {DROP, ressourcesString})
	 i = i + 1
      end
   end
   Queue.push(dataQueue, {INCANTATION, nil})
   return true
end

function moveDarracq()
   local move = math.random(0, 3)
   if (move == 0) then
      Queue.push(dataQueue, {RIGHT, nil})
   elseif (move == 1) then
      Queue.push(dataQueue, {MOVE, nil})
   end
   Queue.push(dataQueue, {MOVE, nil})
end

function onElevate()
   if IA:CanElevate() == false then
      return false
   end
   print("GetNbNeededPlayers  "..IA:GetNbNeededPlayers())
   if IA:GetNbNeededPlayers() ~= IA:GetSightAt(0):GetNbOf(PLAYER) + 1 then
      Queue.push(dataQueue, { BROADCAST, "elevate "..IA:GetLevel() })
      return false
   end
   return true
end

function onSeeAndAct()
   local hasTakeSomething = false

   for ressources, ressourcesString in ipairs(objectOnMap) do
      if (ressources == FOOD
      or needRessource(ressources)) and IA:GetSightAt(0):HasObject(ressources) then
	 Queue.push(dataQueue, {TAKE, ressourcesString})
	 hasTakeSomething = true
      end
   end

   if onElevate() then
      for ressources, ressourcesString in ipairs(objectOnMap) do
	 if IA:GetSightAt(0):HasObject(ressources) then
	    Queue.push(dataQueue, {TAKE, ressourcesString})
	    hasTakeSomething = true
	 end
      end
      if elevateDarracq() == false then
	 moveDarracq()
      end
   elseif (hasTakeSomething == false) then
      moveDarracq()
   else
      moveDarracq()
   end

   --local i = 1
   --while IA:GetSightAt(i) do
   --   Queue.push(dataQueue, {MOVE, nil})
   --   i = i + 1
   --end
end

function OnUpdate()
   
   if canAct == false then
      return NONE
   end

   local value = Queue.pop(dataQueue)

   onSeeAndAct()

   if value then
      canAct = false
      if (value[2]) then
	 IA:SetParameter(value[2])
      end

      print(value[2])
      if value[1] == INCANTATION then
	 canAct = true
      end
      return value[1]
   end

   return NONE
end

function OnReceive(requestCode, responseServer)
   if (serverAnswer[requestCode]) then
      serverAnswer[requestCode](requestCode, responseServer)
   else
      print(requestCode)
      print(responseServer)
   end
end
