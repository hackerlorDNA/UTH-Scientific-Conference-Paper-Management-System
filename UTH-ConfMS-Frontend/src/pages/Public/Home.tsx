import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { ViewState } from '../../App';
import { Hero } from './Hero';
import conferenceApi, { ConferenceDto } from '../../services/conferenceApi';

interface HomeProps {
  onNavigate?: (view: ViewState) => void;
}

export const Home: React.FC<HomeProps> = ({ onNavigate }) => {
  const [recentConferences, setRecentConferences] = useState<ConferenceDto[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const fetchConferences = async () => {
      try {
        const response = await conferenceApi.getConferences(undefined, 1, 3); // Lấy 3 hội nghị mới nhất
        if (response.success && response.data) {
          setRecentConferences(response.data.items);
        }
      } catch (error) {
        console.error('Failed to fetch conferences', error);
      } finally {
        setLoading(false);
      }
    };
    fetchConferences();
  }, []);

  return (
    <>
      <Hero
        onSubmitPaper={() => onNavigate?.('conference-list')}
        onViewCallForPapers={() => onNavigate?.('about-us')}
      />

      {/* Section: Các hội nghị đang diễn ra */}
      <div className="w-full bg-white dark:bg-background-dark py-16 px-5 md:px-10 flex justify-center border-b border-border-light dark:border-border-dark">
        <div className="layout-content-container flex flex-col max-w-[1200px] flex-1">
          <div className="flex justify-between items-center mb-10">
            <h2 className="text-2xl md:text-3xl font-bold text-primary tracking-tight">
              Các hội nghị đang diễn ra
            </h2>
            <Link
              to="/conferences"
              className="text-primary font-bold text-sm hover:underline flex items-center gap-1"
            >
              Xem tất cả <span className="material-symbols-outlined text-sm">arrow_forward</span>
            </Link>
          </div>

          {loading ? (
            <div className="flex justify-center py-10">
              <div className="animate-spin rounded-full h-8 w-8 border-t-2 border-primary"></div>
            </div>
          ) : recentConferences.length > 0 ? (
            <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
              {recentConferences.map((conf) => (
                <div key={conf.conferenceId} className="group flex flex-col bg-white dark:bg-card-dark rounded-xl border border-border-light dark:border-border-dark shadow-sm hover:shadow-xl transition-all duration-300 overflow-hidden">
                  <div className="h-3 bg-primary/80 group-hover:bg-primary transition-colors"></div>
                  <div className="p-6 flex flex-col flex-1">
                    <div className="mb-4 flex items-start justify-between">
                      <span className="inline-block px-3 py-1 rounded-lg bg-blue-50 dark:bg-blue-900/20 text-primary text-xs font-bold tracking-wider">
                        {conf.acronym}
                      </span>
                      <span className="text-xs font-medium text-text-sec-light dark:text-text-sec-dark bg-gray-100 dark:bg-gray-800 px-2 py-1 rounded">
                        {new Date(conf.startDate).getFullYear()}
                      </span>
                    </div>

                    <h3 className="text-lg font-bold text-text-main-light dark:text-text-main-dark mb-3 line-clamp-2 group-hover:text-primary transition-colors">
                      {conf.name}
                    </h3>

                    <div className="flex flex-col gap-2 mb-6">
                      <div className="flex items-center gap-2 text-sm text-text-sec-light dark:text-text-sec-dark">
                        <span className="material-symbols-outlined text-[18px]">calendar_month</span>
                        <span>{new Date(conf.startDate).toLocaleDateString('vi-VN')}</span>
                      </div>
                      <div className="flex items-center gap-2 text-sm text-text-sec-light dark:text-text-sec-dark">
                        <span className="material-symbols-outlined text-[18px]">location_on</span>
                        <span className="truncate">{conf.location || 'Online / UTH'}</span>
                      </div>
                    </div>

                    <div className="mt-auto pt-4 border-t border-dashed border-border-light dark:border-border-dark">
                      <Link
                        to={`/conferences/${conf.conferenceId}`}
                        className="block w-full py-2 rounded-lg bg-gray-50 dark:bg-gray-800 hover:bg-primary hover:text-white text-text-main-light dark:text-text-main-dark font-medium text-sm transition-all duration-200 text-center"
                      >
                        Xem chi tiết
                      </Link>
                    </div>
                  </div>
                </div>
              ))}
            </div>
          ) : (
            <div className="text-center text-gray-500 py-10 bg-gray-50 dark:bg-card-dark rounded-xl border border-dashed border-border-light dark:border-border-dark">
              Chưa có hội nghị nào đang diễn ra.
            </div>
          )}
        </div>
      </div>

      {/* Section: Về chúng tôi (Tóm tắt) */}
      <div className="w-full bg-blue-50/50 dark:bg-gray-900/30 py-16 px-5 md:px-10 flex justify-center">
        <div className="layout-content-container flex flex-col md:flex-row items-center gap-12 max-w-[1200px] flex-1">
          <div className="flex-1 space-y-6">
            <h2 className="text-2xl md:text-3xl font-bold text-primary tracking-tight">
              Tại sao chọn UTH-ConfMS?
            </h2>
            <p className="text-text-main-light dark:text-text-main-dark leading-relaxed text-lg">
              Hệ thống quản lý hội nghị khoa học UTH-ConfMS cung cấp giải pháp toàn diện cho việc tổ chức, quản lý và vận hành các hội nghị quy mô quốc tế. Chúng tôi kết nối tri thức, thúc đẩy hợp tác và chia sẻ nghiên cứu.
            </p>
            <div className="grid grid-cols-1 sm:grid-cols-2 gap-4">
              <div className="flex items-center gap-3">
                <span className="w-10 h-10 rounded-full bg-green-100 flex items-center justify-center text-green-600 material-symbols-outlined">speed</span>
                <span className="font-medium">Quy trình nhanh chóng</span>
              </div>
              <div className="flex items-center gap-3">
                <span className="w-10 h-10 rounded-full bg-blue-100 flex items-center justify-center text-blue-600 material-symbols-outlined">security</span>
                <span className="font-medium">Bảo mật tuyệt đối</span>
              </div>
              <div className="flex items-center gap-3">
                <span className="w-10 h-10 rounded-full bg-orange-100 flex items-center justify-center text-orange-600 material-symbols-outlined">groups</span>
                <span className="font-medium">Kết nối chuyên gia</span>
              </div>
              <div className="flex items-center gap-3">
                <span className="w-10 h-10 rounded-full bg-purple-100 flex items-center justify-center text-purple-600 material-symbols-outlined">analytics</span>
                <span className="font-medium">Thống kê chi tiết</span>
              </div>
            </div>
            <Link
              to="/about"
              className="mt-4 px-6 py-3 bg-white border border-border-light text-primary font-bold rounded-lg hover:bg-gray-50 transition-colors shadow-sm inline-block"
            >
              Tìm hiểu thêm về chúng tôi
            </Link>
          </div>
          <div className="flex-1 w-full max-w-[500px]">
            <div className="relative aspect-square rounded-2xl overflow-hidden shadow-2xl rotate-3 hover:rotate-0 transition-transform duration-500">
              {/* Placeholder image for About Us section */}
              <div className="absolute inset-0 bg-gradient-to-br from-blue-600 to-purple-700 opacity-90"></div>
              <div className="absolute inset-0 flex items-center justify-center text-white p-10 text-center">
                <div>
                  <span className="material-symbols-outlined text-[80px] mb-4">school</span>
                  <h3 className="text-2xl font-bold">Nâng tầm nghiên cứu khoa học</h3>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};
