EVENT_001={
	title='title_test1',
	desc='desc_test1',
	
	percondition = function(self)
		return false
	end,

	option={
		op1='op1_test',
		
		process = function(self, op)
			print(' do '..op)
			return 'EVENT_002';
		end
		
	}
}

EVENT_002={
	title='title_test2',
	desc='desc_test2',
	
	percondition = function(self)
		return false
	end,

	option={
		op1='op1_test',
		op2='op2_test',
		
		process = function(self, op)
			print(' do '..op)
		end
		
	}
}

EVENT_003={
	title='title_test3',
	desc='desc_test3',
	
	percondition = function(self)
		return false
	end,

	option={
		op1='op1_test',
		op2='op2_test',
		op3='op3_test',
		
		process = function(self, op)
			print(' do '..op)
		end
		
	}
}

local Inst = CS.MyGame.Inst
local Tools = CS.Tools

EVENT_yinghuoshouxin={
	title='yinghuoshouxin',
	desc='yinghuoshouxin',
	
	percondition = function(self)
		return true
	end,

	option={
		op1='op1_test',
		op2='op2_test',
		op3='op3_test',
		
		process = function(self, op)
			if (op=='op1') then
				print('op1')
			elseif(op=='op2') then
				print('op2')
			elseif(op=='op3') then
				if(Tools.Probability.IsProbOccur(0.5)) then
					return 'EVENT_002';
				end
			end
			print('do '..op)
		end
		
	}
}