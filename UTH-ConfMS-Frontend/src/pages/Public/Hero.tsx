import React from 'react';

interface HeroProps {
  onSubmitPaper?: () => void;       // Hàm gọi khi nhấn nút Nộp bài
  onViewCallForPapers?: () => void; // Hàm gọi khi nhấn nút Xem kêu gọi bài báo
}

export const Hero: React.FC<HeroProps> = ({ onSubmitPaper, onViewCallForPapers }) => {
  return (
    <div className="w-full bg-gradient-to-b from-blue-50 to-white dark:from-gray-900 dark:to-background-dark">
      <div className="flex flex-1 justify-center px-5 md:px-10 lg:px-20 py-20 lg:py-28">
        <div className="layout-content-container flex flex-col max-w-[900px] flex-1 items-center text-center">
          <span className="inline-block px-4 py-1.5 rounded-full bg-blue-100 dark:bg-blue-900/30 text-primary text-xs font-bold tracking-wider uppercase mb-6">
            CỔNG THÔNG TIN KHOA HỌC UTH
          </span>
          <h1 className="text-4xl md:text-5xl lg:text-6xl font-black leading-[1.1] tracking-[-0.02em] text-text-main-light dark:text-text-main-dark mb-6">
            Kết nối Tri thức <br className="hidden md:block" />
            Thúc đẩy Đổi mới Sáng tạo
          </h1>
          <h2 className="text-base md:text-lg font-normal leading-relaxed text-text-sec-light dark:text-text-sec-dark max-w-[700px] mb-10">
            UTH-ConfMS là nền tảng quản lý hội nghị khoa học tập trung. Nơi công bố các công trình nghiên cứu, tổ chức sự kiện học thuật và kết nối cộng đồng nhà khoa học.
          </h2>

          <div className="flex flex-wrap justify-center gap-4">
            <button
              onClick={onSubmitPaper}
              className="flex min-w-[160px] cursor-pointer items-center justify-center gap-2 rounded-full h-12 px-8 bg-primary hover:bg-primary-hover text-white text-base font-bold leading-normal tracking-[0.015em] transition-all shadow-lg hover:shadow-xl hover:-translate-y-1"
            >
              <span className="material-symbols-outlined text-[20px]">search</span>
              <span className="truncate">Khám phá Hội nghị</span>
            </button>
            <button
              onClick={onViewCallForPapers}
              className="flex min-w-[160px] cursor-pointer items-center justify-center gap-2 rounded-full h-12 px-8 bg-white dark:bg-card-dark text-text-main-light dark:text-text-main-dark border border-border-light dark:border-border-dark hover:bg-gray-50 dark:hover:bg-gray-700 text-base font-bold leading-normal tracking-[0.015em] transition-all shadow-sm hover:shadow-md"
            >
              <span className="material-symbols-outlined text-[20px]">info</span>
              <span className="truncate">Về chúng tôi</span>
            </button>
          </div>
        </div>
      </div>
    </div>
  );
};
