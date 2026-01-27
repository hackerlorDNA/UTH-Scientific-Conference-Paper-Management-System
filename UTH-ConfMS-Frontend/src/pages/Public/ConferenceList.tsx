import React, { useEffect, useState } from 'react';
import { ViewState } from '../../App';
import conferenceApi, { ConferenceDto } from '../../services/conferenceApi';

interface ConferenceListProps {
    onNavigate: (view: ViewState) => void;
    onSelectConference?: (id: string) => void;
}

export const ConferenceList: React.FC<ConferenceListProps> = ({ onNavigate, onSelectConference }) => {
    const [conferences, setConferences] = useState<ConferenceDto[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchConferences = async () => {
            try {
                const response = await conferenceApi.getConferences();
                if (response.success && response.data) {
                    setConferences(response.data.items);
                } else {
                    setError('Không thể tải danh sách hội nghị.');
                }
            } catch (err) {
                setError('Đã xảy ra lỗi khi tải dữ liệu.');
            } finally {
                setLoading(false);
            }
        };

        fetchConferences();
    }, []);

    if (loading) {
        return (
            <div className="min-h-[50vh] flex items-center justify-center">
                <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-primary"></div>
            </div>
        );
    }

    if (error) {
        return (
            <div className="min-h-[50vh] flex items-center justify-center text-red-500 font-medium">
                {error}
            </div>
        );
    }

    return (
        <div className="w-full bg-background-light dark:bg-background-dark py-12 px-5 md:px-10 flex justify-center">
            <div className="layout-content-container flex flex-col max-w-[1200px] flex-1">
                <h1 className="text-3xl font-bold text-primary mb-8 text-center uppercase tracking-wide">
                    Danh sách Hội nghị
                </h1>

                {conferences.length === 0 ? (
                    <div className="text-center text-gray-500 py-10">
                        Hiện chưa có hội nghị nào được tạo.
                    </div>
                ) : (
                    <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                        {conferences.map((conf) => (
                            <div
                                key={conf.conferenceId}
                                className="bg-white dark:bg-card-dark rounded-xl border border-border-light dark:border-border-dark shadow-sm hover:shadow-md transition-all overflow-hidden flex flex-col h-full"
                            >
                                <div className="p-6 flex flex-col flex-1">
                                    <div className="mb-4">
                                        <span className="inline-block px-3 py-1 rounded-full bg-blue-100 dark:bg-blue-900/30 text-primary text-xs font-bold tracking-wider">
                                            {conf.acronym}
                                        </span>
                                    </div>
                                    <h3 className="text-xl font-bold text-text-main-light dark:text-text-main-dark mb-2 line-clamp-2">
                                        {conf.name}
                                    </h3>
                                    <div className="flex items-center gap-2 text-sm text-text-sec-light dark:text-text-sec-dark mb-4">
                                        <span className="material-symbols-outlined text-[18px]">calendar_month</span>
                                        <span>{new Date(conf.startDate).toLocaleDateString('vi-VN')}</span>
                                    </div>
                                    <p className="text-sm text-text-sec-light dark:text-text-sec-dark line-clamp-3 mb-6 flex-1">
                                        {conf.description || 'Chưa có mô tả.'}
                                    </p>
                                    <button
                                        onClick={() => {
                                            if (onSelectConference) onSelectConference(conf.conferenceId);
                                            onNavigate('conference-details');
                                        }}
                                        className="w-full mt-auto cursor-pointer rounded-lg h-10 bg-primary/10 hover:bg-primary/20 text-primary text-sm font-bold transition-colors"
                                    >
                                        Xem chi tiết
                                    </button>
                                </div>
                            </div>
                        ))}
                    </div>
                )}
            </div>
        </div>
    );
};
