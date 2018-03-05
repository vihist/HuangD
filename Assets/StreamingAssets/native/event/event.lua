event={
	id='EVENT_001',
	title='title_test',
	desc='desc_test',
	
	option={
		op1='op1_test',
		op2='op2_test',
		
		process = function(self, op)
			print(event.title..' do '..op)
			local TestClass = CS.TestClass
			local testobj = TestClass();
			testobj.a=3
			testobj:inc()
			return testobj.a
		end
		
	}
		
}