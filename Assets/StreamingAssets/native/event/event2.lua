event={
	id='EVENT_002',
	title='title_test2',
	desc='desc_test2',
	
	option={
		op1='op1_test2',
		op2='op2_test2',
		
		process = function(self, op)
			print(event.title..' do '..op)
			local TestClass = CS.TestClass
			local testobj = TestClass();
			testobj.a=4
			testobj:inc()
			return testobj.a
		end
	}	
}