
import React, { useState } from 'react';
import { ViewState } from '../../App';

interface ForgotPasswordProps {
  onNavigate: (view: ViewState) => void;
}

export const ForgotPassword: React.FC<ForgotPasswordProps> = ({ onNavigate }) => {
  const [email, setEmail] = useState('');
  const [isSubmitted, setIsSubmitted] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setIsLoading(true);
    
    // Simulate API delay
    setTimeout(() => {
      setIsLoading(false);
      setIsSubmitted(true);
    }, 1500);
  };

  return (
    <div className="flex flex-col items-center justify-center grow py-12 px-4 bg-background-light dark:bg-background-dark">
      <div className="w-full max-w-md bg-white dark:bg-card-dark rounded-2xl shadow-xl border border-border-light dark:border-border-dark overflow-hidden">
        
        {/* Header */}
        <div className="px-8 pt-8 pb-6 text-center">
            <div className="size-12 text-primary mx-auto mb-4">
                <span className="material-symbols-outlined text-[48px]">lock_reset</span>
            </div>
            <h2 className="text-2xl font-bold text-text-main-light dark:text-text-main-dark mb-1">Khôi phục mật khẩu</h2>
            <p className="text-text-sec-light dark:text-text-sec-dark text-sm">Nhập email của bạn để nhận mật khẩu mới</p>
        </div>

        {/* Content */}
        <div className="px-8 pb-8">
            {!isSubmitted ? (
                <form className="flex flex-col gap-5" onSubmit={handleSubmit}>
                    <div className="flex flex-col gap-1.5">
                        <label className="text-sm font-semibold text-text-main-light dark:text-text-main-dark" htmlFor="email">Email tài khoản</label>
                        <div className="relative">
                            <span className="absolute left-3 top-1/2 -translate-y-1/2 text-text-sec-light">
                                <span className="material-symbols-outlined text-[20px]">mail</span>
                            </span>
                            <input 
                                id="email"
                                type="email" 
                                value={email}
                                onChange={(e) => setEmail(e.target.value)}
                                className="w-full h-11 pl-10 pr-4 rounded-lg border border-border-light dark:border-border-dark bg-background-light dark:bg-background-dark text-text-main-light dark:text-text-main-dark text-sm focus:ring-2 focus:ring-primary focus:border-transparent outline-none transition-all"
                                placeholder="example@gmail.com"
                                required
                            />
                        </div>
                    </div>

                    <button 
                        type="submit"
                        disabled={isLoading}
                        className="w-full h-11 rounded-lg bg-primary hover:bg-primary-hover text-white font-bold text-sm shadow-md hover:shadow-lg transition-all mt-2 disabled:opacity-70 disabled:cursor-not-allowed flex items-center justify-center gap-2"
                    >
                        {isLoading ? (
                            <>
                                <span className="w-4 h-4 border-2 border-white/30 border-t-white rounded-full animate-spin"></span>
                                <span>Đang xử lý...</span>
                            </>
                        ) : "Gửi yêu cầu phục hồi"}
                    </button>
                    
                    <button 
                        type="button"
                        onClick={() => onNavigate('login')}
                        className="text-sm font-medium text-text-sec-light hover:text-primary transition-colors flex items-center justify-center gap-1"
                    >
                        <span className="material-symbols-outlined text-[18px]">arrow_back</span>
                        Quay lại đăng nhập
                    </button>
                </form>
            ) : (
                <div className="flex flex-col items-center gap-6 py-4 animate-in fade-in zoom-in duration-300">
                    <div className="w-16 h-16 bg-green-100 dark:bg-green-900/30 text-green-600 dark:text-green-400 rounded-full flex items-center justify-center">
                        <span className="material-symbols-outlined text-[36px]">mark_email_read</span>
                    </div>
                    <div className="text-center space-y-2">
                        <h3 className="font-bold text-lg">Yêu cầu đã được gửi!</h3>
                        <p className="text-sm text-text-sec-light dark:text-text-sec-dark leading-relaxed">
                            Mật khẩu mới đã được gửi về email <span className="font-bold text-text-main-light dark:text-text-main-dark">{email}</span>. Vui lòng kiểm tra hộp thư đến hoặc thư rác (Spam).
                        </p>
                    </div>
                    <button 
                        onClick={() => onNavigate('login')}
                        className="w-full h-11 rounded-lg bg-primary hover:bg-primary-hover text-white font-bold text-sm shadow-md transition-all"
                    >
                        Quay lại đăng nhập ngay
                    </button>
                </div>
            )}
        </div>

        <div className="px-8 py-4 bg-gray-50 dark:bg-background-dark/50 border-t border-border-light dark:border-border-dark text-center">
             <p className="text-xs text-text-sec-light dark:text-text-sec-dark italic">Hệ thống hỗ trợ khôi phục qua Google Auth và SMTP.</p>
        </div>
      </div>
    </div>
  );
};