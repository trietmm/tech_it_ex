step 1: nhân viên nhập thông tin và click button tạo => chương trình sẽ tự động trả về bộ câu hỏi dành cho nhân viên

Tạo Candidate
	ICandidate.Register
		Tạo candidate và trả về id của candidate
		
Generate Question cho Candidate
	IGenerate.GenerateQuestion
		tạo ra bộ question cho candidate
		lưu vào trong database toàn bộ dữ liệu question của candidate
		
		
Step 2: nhân viên nhập câu trả lời đầy đủ và nhấn button save => chương trình sẽ tự động tính toán ra những con điểm và lưu vào trong DB
	ICandidate.CandidateAnswer
		chương trình tự động tính ra câu trả lời của nhân viên là đúng hay sai 
		trừ trường hợp câu hỏi dành cho nhân viên ở dạng text

Step 3: người chấm điểm sẽ nhấn vào danh sách candidate => tìm ra những candidate nào chưa hoàn chỉnh điểm (vì có những câu dạng text buộc người chấm điểm phải check đúng hay sai)


step chuẩn bị : nhập thông tin câu hỏi - nhập thông tin câu trả lời

