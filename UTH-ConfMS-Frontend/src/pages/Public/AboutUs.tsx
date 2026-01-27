import React from 'react';

export const AboutUs: React.FC = () => {
    return (
        <div className="w-full bg-background-light dark:bg-background-dark py-12 px-5 md:px-10 flex justify-center">
            <div className="layout-content-container flex flex-col max-w-[1000px] flex-1">
                <div className="bg-white dark:bg-card-dark rounded-xl border border-border-light dark:border-border-dark p-8 md:p-12 shadow-sm">
                    <h1 className="text-3xl md:text-4xl font-bold text-primary mb-6 text-center">Về Chúng Tôi</h1>

                    <div className="space-y-6 text-text-main-light dark:text-text-main-dark leading-relaxed text-justify">
                        <p className="text-lg font-medium text-center text-text-sec-light dark:text-text-sec-dark mb-8">
                            Hệ thống Quản lý Hội nghị Khoa học (UTH-ConfMS)
                        </p>

                        <h3 className="text-xl font-bold text-primary">Sứ mệnh</h3>
                        <p>
                            UTH-ConfMS được xây dựng với sứ mệnh tối ưu hóa quy trình tổ chức và quản lý các hội nghị khoa học.
                            Chúng tôi cung cấp một nền tảng toàn diện kết nối các nhà nghiên cứu, chuyên gia và ban tổ chức,
                            giúp việc nộp bài, phản biện và công bố kết quả nghiên cứu trở nên minh bạch, nhanh chóng và hiệu quả hơn.
                        </p>

                        <h3 className="text-xl font-bold text-primary">Tính năng nổi bật</h3>
                        <ul className="list-disc pl-6 space-y-2">
                            <li><strong>Quản lý bài báo:</strong> Quy trình nộp bài trực tuyến đơn giản, hỗ trợ đa dạng định dạng.</li>
                            <li><strong>Quy trình phản biện chuyên nghiệp:</strong> Hệ thống phân công phản biện tự động và thủ công, đảm bảo tính khách quan (Double-blind review).</li>
                            <li><strong>Quản lý chương trình:</strong> Sắp xếp lịch trình, phiên họp và diễn giả một cách khoa học.</li>
                            <li><strong>Thống kê & Báo cáo:</strong> Cung cấp cái nhìn tổng quan về số lượng bài nộp, trạng thái phản biện và chất lượng hội nghị.</li>
                        </ul>

                        <h3 className="text-xl font-bold text-primary">Đội ngũ phát triển</h3>
                        <p>
                            Hệ thống được phát triển bởi đội ngũ kỹ sư phần mềm tâm huyết từ Trường Đại học Giao thông vận tải TP.HCM.
                            Chúng tôi không ngừng cải tiến và cập nhật công nghệ mới nhất để mang lại trải nghiệm tốt nhất cho người dùng.
                        </p>

                        <div className="mt-8 pt-8 border-t border-border-light dark:border-border-dark text-center">
                            <p className="text-sm text-text-sec-light dark:text-text-sec-dark">
                                Mọi thắc mắc và góp ý xin vui lòng liên hệ: <br />
                                <span className="font-bold text-primary">support@uth.edu.vn</span> | Hotline: 028 1234 5678
                            </p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};
