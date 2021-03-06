if LMailItem then print("same lua, reload ignore!!!") end

local M =
{
	_mail=nil;
}

function M:new(csobj)
	csobj.luaOnAwake = self.Awake
end

function M:Awake()
	--self.luaItem = self.luaItem:GetComponent("UIItemSlot")
	EventListener.Get(self.luaGain):AddOnClick(function(evt)  self:OnMailGainClick() end)
end

function M:SetData(mail,itemprefab)
	self._mail = mail
	self.luaNick:GetComponent("Text").text = mail.nick
	self.luaTitle:GetComponent("Text").text = mail.title
	self.luaState:SetActive(mail.state~=0)
	if mail.rewards==nil or #mail.rewards < 1 then
		self.luaGain:SetActive(false)
		self.luaReward:SetActive(false)
	else
		self.luaGain:SetActive(mail.state~=2)
		self.luaReward:SetActive(true)
		local mount = self.luaReward.transform
		GHelper.DestroyChilds(mount)
		for i=1,#mail.rewards do
			local reward = mail.rewards[i]
			local it = GameObject.Instantiate(itemprefab)
			it.transform:SetParent(mount, false)
			local item = LTBItem[reward.id]
			it:GetComponent(typeof(UIItemSlot)):SetData(item.icon,item.name,reward.num)
		end
	end
end

function M:SetState(state)
	local mail = self._mail
	if mail.state >= state then return end
	mail.state = state
	self.luaState:SetActive(state~=0)
	self.luaGain:SetActive(mail.state~=2 and mail.rewards~=nil and #mail.rewards > 0)
end

function M:OnMailGainClick( ... )
	local win = WindowMgr.single:GetWindow("MailWindow").mLO.mLT
	win:GainReward(self._mail)
end
--========================
LMailItem = M
createClass("LMailItem",LMailItem)