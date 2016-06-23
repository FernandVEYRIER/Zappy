--
-- Created by IntelliJ IDEA.
-- User: gouet_v
-- Date: 6/8/16
-- Time: 12:50 PM
-- To change this template use File | Settings | File Templates.
--

canAct = true
serverAnswer = {}
msg = nil

local path = "src_client_ai/lua/"

queue = require (path.."Queue")

-- object on map
local objectOnMap = {}

local broadCastMsg = {}

local objToSend = {}

function OnStart()
   serverAnswer[MOVE] = onCallMove
   serverAnswer[LEFT] = onCallMove
   serverAnswer[RIGHT] = onCallMove
   serverAnswer[TAKE] = onTakeObj
   serverAnswer[DROP] = onDropObj
   serverAnswer[LAYEGG] = nil
   serverAnswer[BROADCAST] = onReceiveBroadCast

   queue.new()
   objectOnMap[FOOD] = "nourriture"
   objectOnMap[LINEMATE] = "linemate"
   objectOnMap[DERAUMERE] = "deraumere"
   objectOnMap[SIBUR] = "sibur"
   objectOnMap[MENDIANE] = "mendiane"
   objectOnMap[PHIRAS] = "phiras"
   objectOnMap[THYSTAME] = "thystame"

   broadCastMsg[1] = "Vous la bas ! Je vous vends "
   broadCastMsg[2] = "HEY FRERE ! Tu veux du bon "
   broadCastMsg[3] = "Et toi ! VIENS DONC QUE J'TE... VENDS DU "
   broadCastMsg[4] = "Je vends pas chère "

   objToSend[1] = "deraumere"
   objToSend[2] = "sibur"
   objToSend[3] = "mendiane"
   objToSend[4] = "phiras"
   objToSend[5] = "thystame"
   objToSend[6] = "linemate"
end

function onReceiveBroadCast(requestCode, responseServer)

   if (responseServer == "ok") then
      canAct = true
   end
end

function onCallMove(requestCode, responseServer)
   canAct = true
end

function takeObj()
   local i = 0
   for i, v in pairs(objectOnMap) do
      if (IA:GetSightAt(0):HasObject(i)) then
	 -- IA:SetParameter(v)
	 return (v)
      end
   end
   return (nil)
end

function onTakeObj(requestCode, responseServer)
   print(requestCode, responseServer)
   canAct = true
end

function onDropObj(requestCode, responseServer)
   canAct = true
end

local etableIsCreated = false
local msgOnAction = nil
local objSell = nil
local objDrop = nil
local nbrFoodForBuy = 10

function getObjFromString(objName)
   for i, v in pairs(objectOnMap) do
      if (v == objName) then
	 return (i)
      end
   end
   return nil
end

function OnUpdate()

   if canAct == false then
      return NONE
   end

   if (msgOnAction) then
      -- on a un msg à transmettre
      IA:SetParameter(msgOnAction)
      msgOnAction = nil
      return BROADCAST
   end

   if (objDrop) then
      -- On drop OBJ car on a pu vendre l OBJ
      IA:SetParameter(objDrop)
      objDrop = nil
      objSell = nil

      return DROP
   end

   -- On essaye de voir si on peut passer en mode vente
   if (IA:GetInventory():GetNbOf(FOOD) > nbrFoodForBuy) then

      -- On est dans la phase de vente
      nbrFoodForBuy = 5
      local obj = takeObj()
      if (obj) then
	 if (etableIsCreated and obj == "nourriture") then

	    -- On a vendu notre merde
	    msgOnAction = "merci Johny !"
	    objDrop = objSell
	 elseif (etableIsCreated) then

	    -- Le mec essaye de nous arnaquer
	    msgOnAction = "Tu m'as cru pour une bourrique ?"
	 end

	 -- On créer notre étable
	 IA:SetParameter(obj)
	 canAct = false
	 return TAKE
      end

      -- On commence à vendre
      local i = 0
      while (objSell == nil and i < 10) do
	 local i = math.random(1, #objToSend)
	 objSell = objToSend[i]
	 if getObjFromString(objSell) then
	    if (IA:GetInventory():GetNbOf(getObjFromString(objSell)) == 0) then
	       objSell = nil
	    end
	 end
	 i = i + 1
      end

      if (objSell == nil) then
	 return NONE
      end

      etableIsCreated = true

      IA:SetParameter(broadCastMsg[(math.random(1, #broadCastMsg))] .. objSell) -- a modifier
      canAct = false

      return BROADCAST

      -- end
   end


   -- On se déplace pour trouver des objs rares
   etableIsCreated = false
   nbrFoodForBuy = 10
   objSell = nil

   if (IA:GetSightAt(0)) then

      -- On est dans la phase de récuperation des objs
      local obj = takeObj()
      if (obj) then

	 -- On a bien a un obj
	 IA:SetParameter(obj)
	 canAct = false
	 return TAKE
      else
	 -- On en a pas
	 canAct = false

	 local mv = math.random(1, 4)
	 print(mv)
	 if mv == 2 then
	    return LEFT
	 elseif mv == 1 then
	    return RIGHT
	 end
	 return MOVE
      end
      -- end phase de récuperation des objs

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
