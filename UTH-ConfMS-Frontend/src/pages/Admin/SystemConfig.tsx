import React from 'react';
import { ViewState } from '../../App';

interface Props { onNavigate: (view: ViewState) => void; }

export const SystemConfig: React.FC<Props> = ({ onNavigate }) => {
  return (
    <div className="p-8 max-w-4xl mx-auto">
      <button onClick={() => onNavigate('admin-dashboard')} className="mb-6 flex items-center text-blue-600 hover:underline">
        <span className="material-symbols-outlined mr-1">arrow_back</span> Quay lại Dashboard
      </button>
      <h1 className="text-2xl font-bold mb-8">Cấu hình hệ thống</h1>
      
      <div className="space-y-6 bg-white p-6 rounded-xl border border-gray-200 shadow-sm">
        <div>
          <label className="block text-sm font-medium text-gray-700 mb-2">Tên hội nghị</label>
          <input type="text" className="w-full p-2 border rounded-lg focus:ring-2 focus:ring-blue-500 outline-none" defaultValue="UTH Scientific Conference 2026" />
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">Hạn nộp bài</label>
            <input type="date" className="w-full p-2 border rounded-lg outline-none focus:ring-2 focus:ring-blue-500" />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-2">Hạn gửi phản biện</label>
            <input type="date" className="w-full p-2 border rounded-lg outline-none focus:ring-2 focus:ring-blue-500" />
          </div>
        </div>
        <button className="bg-blue-600 text-white px-6 py-2 rounded-lg hover:bg-blue-700 transition-colors">Lưu cấu hình</button>
      </div>
    </div>
  );
};