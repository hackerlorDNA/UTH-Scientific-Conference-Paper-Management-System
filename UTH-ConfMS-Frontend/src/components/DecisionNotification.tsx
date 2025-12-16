
import React from 'react';
import { AIBadge } from './AIBadge';

export const DecisionNotification: React.FC = () => {
  return (
    <div className="w-full bg-background-light dark:bg-background-dark py-12 px-5 flex justify-center">
        <div className="w-full max-w-[800px] bg-white dark:bg-card-dark rounded-xl border border-border-light shadow-lg overflow-hidden">
            <div className="bg-green-600 p-6 text-white text-center">
                <div className="w-16 h-16 bg-white/20 rounded-full flex items-center justify-center mx-auto mb-4">
                    <span className="material-symbols-outlined text-[32px]">check_circle</span>
                </div>
                <h1 className="text-2xl font-bold">Chúc mừng! Bài báo của bạn đã được Chấp nhận</h1>
                <p className="opacity-90 mt-2">Paper ID: #156</p>
            </div>
            
            <div className="p-8">
                <div className="mb-6">
                    <h2 className="text-lg font-bold mb-2">Thông tin chi tiết</h2>
                    <p className="text-sm text-text-sec-light mb-1"><span className="font-bold text-text-main-light">Tiêu đề:</span> A Survey on Smart Grid Security Protocols</p>
                    <p className="text-sm text-text-sec-light"><span className="font-bold text-text-main-light">Quyết định:</span> Accepted for Oral Presentation</p>
                </div>

                <div className="bg-gray-50 dark:bg-gray-800 p-4 rounded-lg border border-border-light mb-6">
                    <div className="flex justify-between items-center mb-3">
                        <h3 className="font-bold text-sm">Tổng hợp đánh giá</h3>
                        <AIBadge label="AI Summarized" size="sm" />
                    </div>
                    <p className="text-sm text-text-sec-light leading-relaxed">
                        Bài báo có cấu trúc tốt, nội dung khảo sát đầy đủ và cập nhật. Các phản biện đánh giá cao tính thực tiễn của đề tài. Tuy nhiên, tác giả cần chỉnh sửa lại một số lỗi ngữ pháp nhỏ và định dạng tài liệu tham khảo theo đúng chuẩn IEEE trước khi nộp bản Camera-ready.
                    </p>
                </div>

                <div className="flex flex-col gap-3">
                    <button className="w-full py-3 bg-primary text-white font-bold rounded-lg hover:bg-primary-hover shadow-sm transition-colors">
                        Nộp bản Camera-ready (Final Version)
                    </button>
                    <button className="w-full py-3 bg-white border border-border-light text-text-main-light font-bold rounded-lg hover:bg-gray-50 transition-colors">
                        Tải về nhận xét chi tiết (PDF)
                    </button>
                </div>
            </div>
        </div>
    </div>
  );
};
