Các bước cài đặt để chạy project MemberCardManagement:
1. Cài Microsoft SQL Server Management Studio tại:
https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15

2. Cài Visual Studio tại:
https://visualstudio.microsoft.com/

3. Mở Microsoft SQL Server Management Studio, restore file database MemberCardManagement.bak, đặt tên database là MemberCardManagement

4. Chạy project MemberCardManagement:
- Mở file solution MemberCardManagementV1.sln
- Sau khi mở ấn tổ hợp phím Ctrl + F5 để chạy project

Sau khi đã chạy project thành công thì làm theo hướng dẫn sau để thực hiện các yêu cầu của bài tập:

Lưu ý: Tôi có điều chỉnh 1 số yêu cầu cho phù hợp với nghiệp vụ thực tế

1. Chỉnh sửa cấu hình tích điểm lưu trữ vào database:
- Ấn vào menu Chính sách tích điểm trên navbar
- Ấn vào Thêm
- Tại đây bạn có thể thêm mới chính sách tích điểm (cấu hính tích điểm) với các thông tin:
+ Tên chính sách tích điểm (bắt buộc)
+ Giá trị tích điểm (VND): nhập vào số tiền X với 1 điểm = {X} VND
+ Là chính sách mặc định: = true thì đây sẽ là chính sách tích điểm mặc định đối với những hạng thẻ không thiết lập chính sách tích điểm
						  = false hoặc not set thì không phải là chính sách tích điểm mặc định
- Sau khi nhập các thông tin thì ấn Cất để lưu trữ chính sách tích điểm vào database

2. Tạo hạng thẻ:
- Ấn vào menu Hạng thẻ
- Ấn vào Thêm
- Tại đây bạn có thể thêm mới hạng thẻ với các thông tin:
+ Tên hạng thẻ
+ Doanh thu lên hạng (VND)
+ Thời hạn (ngày)
+ Chiết khấu (%)
+ Chính sách tích điểm: chọn chính sách tích điểm áp dụng cho hạng thẻ này (các giao dịch của các thẻ tích điểm thuộc hạng thẻ này sẽ áp dụng chính sách tích điểm này)
+ Là hạng thẻ mặc định (chức năng đang phát triển)
- Sau khi nhập các thông tin thì ấn Cất để lưu trữ hạng thẻ vào database

3. Tạo thẻ tích điểm:
- Ấn vào menu Thẻ tích điểm
- Ấn vào Thêm
- Tại đây bạn có thể thêm mới thẻ tích điểm với các thông tin:
+ Mã thẻ
+ Số điện thoại
+ Hạng thẻ: (thường sẽ bắt buộc phải là 1 hạng thẻ nào đó, mặc định là thẻ thành viên (hiện tại đang thi công chức năng hạng thẻ mặc định nên đang mặc định là hạng thẻ đầu tiên trong danh sách))
+ Số điểm
+ Doanh thu
+ Ngày bắt đầu
+ Ngày kết thúc
- Sau khi nhập các thông tin thì ấn Cất để lưu trữ thẻ tích điểm vào database

4. Ghi nhận lại các giao dịch, xử lý tích điểm cho khách hàng, thay đổi thông tin hạng thẻ của khách hàng tương ứng:
- Ấn vào menu Thẻ tích điểm để tạo mới thẻ tích điểm (nếu chưa có)
- Sau khi tạo thành công thẻ tích điểm, ấn vào menu Giao dịch tích điểm
- Ấn vào Thêm
- Tại đây bạn có thể thêm mới giao dịch tích điểm với các thông tin:
+ Thẻ tích điểm: chọn mã thẻ tích điểm muốn thực hiện giao dịch
+ Số doanh thu điều chỉnh (VND): bắt buộc
+ Số điểm điều chỉnh: 
=> Trường này không cần nhập vì phần mềm sẽ thực hiện tính toán số điểm của giao dịch này dựa vào:
- Số doanh thu của giao dịch
- Chính sách tích điểm của hạng thẻ của thẻ tích điểm đó
- % chiết khấu của hạng thẻ đó
- Điều kiện là thẻ tích điểm đó phải còn hiệu lực tính đến ngày thực hiện giao dịch
- Sau khi nhập các thông tin thì ấn Cất để lưu trữ giao dịch tích điểm vào database
- Sau khi lưu thành công giao dịch tích điểm sẽ tự động xử lý tích điểm cho khách hàng và thay đổi hạng thẻ của khách hàng theo các thiết lập trước đó

Các câu hỏi bổ sung:
1. Nếu giao dịch được tổng hợp từ các hệ thống khác và định kỳ được tải lên hệ thống tích điểm, thì xử lý như thế nào trong trường hợp cấu hình quy đổi điểm bị thay đổi giữa các lần giao dịch của khách hàng.
VD: Khách hàng có 3 giao dịch mua hàng vào buổi sáng, và 2 giao dịch vào buổi chiều cùng ngày. Cấu hình quy đổi điểm được thay đổi vào 12h trưa ngày hôm đó. 22h cuối ngày thì dữ liệu mới được tổng hợp về hệ thống tích điểm.

Trả lời: trường hợp này ít khi xảy ra vì đã số khách hàng đều được thông báo có bao nhiêu điểm trong thẻ sau khi thực hiện giao dịch, nếu có trường hợp này thì có các cách xử lý sau:
Cách 1: 
- Lưu lại lịch sử thay đổi cấu hình tích điểm vào database, mỗi lần thay đổi cấu hình tích điểm sẽ lưu trữ thêm 1 dòng vào database có ngày thay đổi
- Khi tổng hợp dữ liệu về hệ thống tích điểm sẽ dựa vào thời gian của các cấu hính tích điểm và thời gian thực hiện giao dịch của khách hàng để tích điểm cho khách hàng
VD: nếu cấu hình tích điểm được tạo lúc 8h sáng, cấu hính tích điểm thay đổi vào 12h trưa => các giao dịch diễn ra sau 8h sáng và trước 12h trưa sẽ được áp dụng cấu hình tích điểm tạo lúc 8h sáng

Cách 2:
- Đỗi với mỗi giao dịch, tạo thêm 1 trường lưu lại cấu hình tích điểm hiện tại của phần mềm lúc đó (trường này có thể là kiểu json)
- Khi tổng hợp lại các giao dịch để tích điểm chỉ cần đọc cấu hình tích điểm từ trường này ra

2. Để tăng hiệu năng hệ thống khi có rất nhiều giao dịch của nhiều khách hàng đồng thời thì có giải pháp nào không?
- Nói chung là có nhiều yếu tố ảnh hưởng đến hiệu năng hệ thống
- Tùy vào công nghệ, kiến trúc mà mỗi dự án áp dụng
- 1 số lời khuyên cơ bản đó là: code phải tối ưu, cơ sở hạ tầng mạng phải tốt, ...

Lưu ý: còn nhiều chức năng tôi đang phát triển trong tương lai do hiện tại không có đủ thời gian để hoàn thiện, project này tôi có điều chỉnh lại để mọi người dễ dùng hơn và phù hợp với nghiệp vụ thực tế

Mọi thắc mắc vui lòng liên hệ với số điện thoại 0328159040 để được giải đáp.