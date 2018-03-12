event={
	title='title_test',
	desc='desc_test',
	
	percondition = function(self, op)
		return true
	end,

	option={
		op1='op1_test',
		op2='op2_test',
		
		process = function(self, op)
			print(event.title..' do '..op)
		end
		
	}
}