--
-- Created by tavern_d
-- User: tavern_d
--

Queue = {};
canAct = true;
oeuf = 0;

function Queue.new()
    return { first = 0, last = -1 };
end

function Queue.pushBack(list, value)
    local topush = list.first - 1;
    list.first = topush;
    list[topush] = value;
end

function Queue.pushLeft(list, value)
    local last = list.last + 1
    list.last = last
    list[last] = value
end

function Queue.pop(list)
    local last = list.last;
    if list.first > last then
        return nil;
    end
    local value = list[last];
    list[last] = nil;
    list.last = last - 1;
    return value;
end

actionList = Queue.new();

function OnStart()
end

function CreatePath(case)
    local path = Queue.new();
    local f = 3;
    local n = 1;
    local i = 1;
    local y = 0;
    if case == 0 then
        for w = 0, (IA:GetSightAt(y):GetNbOf(FOOD) - 1) do
            Queue.pushBack(path, "TAKE nourriture");
        end
        return path;
    end
    while n <= IA:GetLevel() do
        if i <= case and case <= i + n * 2 then
            for j = 0, n - 1, 1 do
                Queue.pushBack(path, "MOVE");
                y = y + (j + 1) * 2;
                if IA:GetSightAt(y):HasObject(FOOD) == true then
                    for w = 0, (IA:GetSightAt(y):GetNbOf(FOOD) - 1) do
                        Queue.pushBack(path, "TAKE nourriture");
                    end
                end
            end
            if case == y then
                return path;
            elseif case < y then
                Queue.pushBack(path, "LEFT");
                while y > case do
                    Queue.pushBack(path, "MOVE");
                    y = y - 1;
                    if IA:GetSightAt(y):HasObject(FOOD) == true then
                        for w = 0, (IA:GetSightAt(y):GetNbOf(FOOD) - 1) do
                            Queue.pushBack(path, "TAKE nourriture");
                        end
                    end
                end
                return path;
            else
                Queue.pushBack(path, "RIGHT")
                while y < case do
                    Queue.pushBack(path, "MOVE");
                    y = y + 1;
                    if IA:GetSightAt(y):HasObject(FOOD) == true then
                        for w = 0, (IA:GetSightAt(y):GetNbOf(FOOD) - 1) do
                            Queue.pushBack(path, "TAKE nourriture");
                        end
                    end
                end
                return path;
            end
            n = n + 1;
        end
        i = i + f;
        f = f + 2;
    end
end

function OnUpdate()
    if oeuf >= 20 then
        return;
    end
    if (canAct) then
        local food = IA:GetInventory():GetNbOf(FOOD);
        local lvl = IA:GetLevel();
        local i = 3;
        for n = 2, lvl do
            i = i + (n * 2 + 1);
        end
        -- vider la queue

        local action = Queue.pop(actionList);
        if action ~= nil then
            if string.find(action, "MOVE") ~= nil then
                canAct = false;
                return MOVE;
            elseif string.find(action, "RIGHT") ~= nil then
                canAct = false;
                return RIGHT;
            elseif string.find(action, "LEFT") ~= nil then
                canAct = false;
                return LEFT;
            elseif string.find(action, "RIGHT") ~= nil then
                canAct = false;
                return RIGHT;
            elseif string.find(action, "INCANTATION") ~= nil then
                canAct = false;
                return INCANTATION;
            elseif string.find(action, "LAYEGG") ~= nil then
                canAct = false;
                return LAYEGG;
            elseif string.find(action, "TAKE") ~= nil then
                if string.find(action, "nourriture") ~= nil then
                    IA:SetParameter("nourriture");
                elseif string.find(action, "linemate") ~= nil then
                    IA:SetParameter("linemate");
                elseif string.find(action, "deraumere") ~= nil then
                    IA:SetParameter("deraumere");
                elseif string.find(action, "sibur") ~= nil then
                    IA:SetParameter("sibur");
                elseif string.find(action, "mendiane") ~= nil then
                    IA:SetParameter("mendiane");
                elseif string.find(action, "phiras") ~= nil then
                    IA:SetParameter("phiras");
                elseif string.find(action, "thystame") ~= nil then
                    IA:SetParameter("thystame");
                end
                canAct = false;
                return TAKE;
            elseif string.find(action, "DROP") ~= nil then
                canAct = false;
                return DROP;
            end
        end

        if food < 7 then
            -- recherche de food
            local n = 0;
            local find = false;
            while n <= i and find == false do
                local ncase = IA:GetSightAt(n);
                if ncase == nil then
                    return;
                end
                if ncase:HasObject(FOOD) == true then
                    find = true;
                    actionList = CreatePath(n);
                    return;
                end
                n = n + 1;
            end
            if n > i then
                local ran = math.random(0, 2);
                if ran == 0 then
                    Queue.pushBack(actionList, "LEFT");
                elseif ran == 1 then
                    Queue.pushBack(actionList, "RIGHT");
                else
                    for j = 0, (lvl - 1) do
                        Queue.pushBack(actionList, "MOVE");
                    end
                end
            end
        else
            canAct = false;
            oeuf = oeuf + 1;
            return (LAYEGG);
        end
    end
end



function OnReceive(request, rep)
    if request == MOVE or request == LEFT or request == RIGHT or request == TAKE or request == DROP or request == LAYEGG then
        canAct = true;
        if rep == "ko" then
            actionList = Queue.new();
        end
        return;
    end
end
