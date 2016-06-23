--
-- Created by IntelliJ IDEA.
-- User: gouet_v
-- Date: 6/8/16
-- Time: 12:50 PM
-- To change this template use File | Settings | File Templates.
--

serverAnswer = {}
canAct = true
objectOnMap = {}

function onCallMove(requestCode, responseServer)
   canAct = true
end

function onCallTake(requestCode, responseServer)
   canAct = true
end

function onCallExpulse(requestCode, responseServer)
   canAct = true
end

function takeObj()
   local i = 0
   for i, v in pairs(objectOnMap) do
      if (IA:GetSightAt(0):HasObject(i)) then
	 return (v)
      end
   end
   return (nil)
end

function onEventWhenMorePlayer()
   local obj = takeObj()
   if (obj) then
      IA:SetParameter(obj)
      return TAKE
   end
   return EXPULSE
end

function onEventWhenMove()
   local i = 1

   while (IA:GetSightAt(i) and i <= 3) do
      if (IA:GetSightAt(i):GetNbOf(PLAYER) >= 1) then

	 if (i == 2) then
	    return MOVE
	 end
      end
      i = i + 1
   end
   local rand = math.random(0, 4)
   if (rand == 0 or rand == 2) then
      return MOVE
   elseif (rand == 1) then
      return LEFT
   else
      return RIGHT
   end
   return MOVE
end

function OnStart()
   serverAnswer[MOVE] = onCallMove
   serverAnswer[LEFT] = onCallMove
   serverAnswer[RIGHT] = onCallMove
   serverAnswer[TAKE] = onCallTake
   serverAnswer[DROP] = nil
   serverAnswer[LAYEGG] = nil
   serverAnswer[BROADCAST] = nil
   serverAnswer[EXPULSE] = onCallExpulse

   objectOnMap[FOOD] = "nourriture"
   objectOnMap[LINEMATE] = "linemate"
   objectOnMap[DERAUMERE] = "deraumere"
   objectOnMap[SIBUR] = "sibur"
   objectOnMap[MENDIANE] = "mendiane"
   objectOnMap[PHIRAS] = "phiras"
   objectOnMap[THYSTAME] = "thystame"
end

function OnUpdate()

   if canAct == false then
      return NONE
   end

   canAct = false

   if (IA:GetSightAt(0):GetNbOf(PLAYER) >= 1) then
      return onEventWhenMorePlayer()
   end
   if (IA:GetSightAt(0):HasObject(FOOD)) then
      IA:SetParameter("nourriture")
      return TAKE
   end

   return onEventWhenMove()
end

function OnReceive(requestCode, responseServer)
   if (serverAnswer[requestCode]) then
      serverAnswer[requestCode](requestCode, responseServer)
   else
      print(requestCode)
      print(responseServer)
   end
end

