EVENT_001={
	title='title_test1',
	desc='desc_test1',
	
	percondition = function(self)
		return true
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
		return true
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

local myGame = CS.MyGame.Inst

EVENT_yinghuoshouxin={
	title='yinghuoshouxin',
	desc='yinghuoshouxin',
	
	percondition = function(self)
		return true
	end,

	option={
		op1='啊啊啊啊',
		op2='op2_test',
		op3='op3_test',
		
		process = function(self, op)
			print(' do '..op)
			myGame.empAge = 10;
		end
		
	}
}