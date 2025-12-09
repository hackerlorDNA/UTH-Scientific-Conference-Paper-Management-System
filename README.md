# UTH-Scientific-Conference-Paper-Management-System
# UTH-ConfMS – Hệ thống Quản lý Bài báo Hội nghị Khoa học UTH

## 📌 1. Thông tin chung
- **Tên dự án:** Hệ thống Quản lý Bài báo Hội nghị Khoa học UTH  
- **Mô tả:** Hệ thống quản lý giấy tờ Hội nghị Nghiên cứu khoa học dành cho Trường ĐH UTH  
- **Tên viết tắt:** UTH-ConfMS

---

## 🌐 2. Bối cảnh & Giải pháp
**Vấn đề:** Quy trình phân mảnh • Dữ liệu trùng lặp • Giao tiếp rời rạc • COI không đồng đều • Báo cáo hạn chế  
**Giải pháp:** Quy trình khép kín (CFP → Nộp bài → Phản biện → Quyết định → Xuất bản)  
**Tính năng chính:** RBAC • SSO • Audit log • Cấu hình linh hoạt

---

## ⚙️ 3. Yêu cầu chức năng
- Thiết lập Hội nghị & CFP  
- Nộp bài & chỉnh sửa  
- Quản lý PC & COI  
- Phân công & đánh giá  
- Quyết định & gửi thông báo  
- Chuẩn bị bản cuối & xuất biên bản  
- Báo cáo & phân tích  

**AI (tùy chọn):** kiểm tra chính tả • tóm tắt trung lập • gợi ý reviewer

---

## 🛡️ 4. Yêu cầu phi chức năng
- **Bảo mật:** HTTPS • RBAC • SSO • Audit log  
- **Riêng tư:** Anonymous / Double-blind • COI  
- **Hiệu năng:** tải cao • caching  
- **I18n:** UI Tiếng Anh / Tiếng Việt  
- **AI Explainability:** giải thích ngắn gọn

---

## 🧱 5. Công nghệ & Sản phẩm
- **Backend:** C# .NET  
- **Database:** PostgreSQL • Redis  
- **Frontend:** ReactJS  
- **API:** OpenAPI / Swagger  

**Sản phẩm:** Web App theo vai trò • Portal quản trị • Portal công cộng • Bộ tài liệu dự án

---

## 🗂️ 6. Nhiệm vụ (Tasks)
- **TP1:** Quản trị & nền tảng  
- **TP2:** Hội nghị & CFP  
- **TP3:** Nộp bài  
- **TP4:** PC & phân công  
- **TP5:** Đánh giá & thảo luận  
- **TP6:** Quyết định & thông báo  
- **TP7:** Bản cuối & kỷ yếu  
- **TP8:** Xây dựng – Triển khai – Kiểm thử  
- **TP9:** Hoàn thiện tài liệu
