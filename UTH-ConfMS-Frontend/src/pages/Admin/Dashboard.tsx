import React from 'react';
import { ViewState } from '../../App';
import { SystemConfig } from './SystemConfig';

interface AdminDashboardProps {
  onNavigate: (view: ViewState) => void;
}

export const AdminDashboard: React.FC<AdminDashboardProps> = ({ onNavigate }) => {
  return (
    <div className="w-full bg-background-light dark:bg-background-dark py-8 px-5 md:px-10 flex justify-center min-h-screen">
      <div className="w-full max-w-[1200px] flex flex-col gap-8">
        <h1 className="text-2xl font-bold text-text-main-light dark:text-text-main-dark">Admin Dashboard</h1>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
          {/* Card 1: Quản lý người dùng */}
          <div className="bg-white dark:bg-card-dark p-6 rounded-xl border border-border-light shadow-sm flex flex-col">
            <span className="material-symbols-outlined text-4xl text-primary mb-2">manage_accounts</span>
            <h3 className="font-bold text-lg">Quản lý người dùng</h3>
            <p className="text-sm text-text-sec-light mt-1">Thêm, sửa, xóa và phân quyền người dùng (Author, Reviewer, Chair).</p>
            <button 
              onClick={() => onNavigate('admin-users')}
              className="mt-auto pt-4 text-sm font-bold text-primary hover:underline text-left flex items-center gap-1"
            >
              Truy cập <span className="material-symbols-outlined text-sm">arrow_forward</span>
            </button>
          </div>

          {/* Card 2: Cấu hình hệ thống */}
          <div className="bg-white dark:bg-card-dark p-8 rounded-xl border border-[#eaecf0] shadow-sm flex flex-col min-h-[220px] transition-all hover:shadow-md group">
                    <div className="w-12 h-12 rounded-xl border border-green-200 flex items-center justify-center mb-6 bg-green-50/50">
                      <span className="material-symbols-outlined text-green-600 text-[30px]">settings</span>
                    </div>
                    <h3 className="font-bold text-[22px] text-[#101828] dark:text-white">Cấu hình hệ thống</h3>
                    <p className="text-[15px] text-[#667085] mt-3">Cài đặt hạn nộp bài, email tự động và tham số hệ thống.</p>
                    <button 
                      onClick={() => onNavigate('admin-config')}
                      className="mt-auto pt-6 text-[15px] font-bold text-primary hover:text-primary-hover flex items-center gap-1.5 w-fit"
                    >
                      Truy cập <span className="material-symbols-outlined text-[18px]">arrow_forward</span>
                    </button>
                </div>

          {/* Card 3: Sao lưu & Dữ liệu */}
          <div className="bg-white dark:bg-card-dark p-6 rounded-xl border border-border-light shadow-sm flex flex-col">
            <span className="material-symbols-outlined text-4xl text-blue-600 mb-2">database</span>
            <h3 className="font-bold text-lg">Sao lưu & Dữ liệu</h3>
            <p className="text-sm text-text-sec-light mt-1">Quản lý cơ sở dữ liệu, lịch sử hoạt động và logs hệ thống.</p>
            <button 
              onClick={() => onNavigate('admin-backup')}
              className="mt-auto pt-4 text-sm font-bold text-primary hover:underline text-left flex items-center gap-1"
            >
              Truy cập <span className="material-symbols-outlined text-sm">arrow_forward</span>
            </button>
          </div>
        </div>


      </div>
    </div>
  );
};