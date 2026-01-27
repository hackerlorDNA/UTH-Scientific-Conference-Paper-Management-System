import React, { useEffect, useState } from 'react';
import conferenceApi, { ConferenceDetailDto } from '../services/conferenceApi';
import { ViewState } from '../App';

interface ConferenceDetailsProps {
  conferenceId?: string;
  onNavigate?: (view: ViewState) => void;
}

export const ConferenceDetails: React.FC<ConferenceDetailsProps> = ({ conferenceId, onNavigate }) => {
  const [conference, setConference] = useState<ConferenceDetailDto | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchConferenceDetails = async () => {
      if (!conferenceId) {
        setError('Không tìm thấy thông tin hội nghị.');
        setLoading(false);
        return;
      }

      try {
        setLoading(true);
        const response = await conferenceApi.getConference(conferenceId);
        if (response.success && response.data) {
          setConference(response.data);
        } else {
          setError('Không thể tải thông tin hội nghị.');
        }
      } catch (err) {
        setError('Đã xảy ra lỗi khi tải dữ liệu.');
        console.error(err);
      } finally {
        setLoading(false);
      }
    };

    fetchConferenceDetails();
  }, [conferenceId]);

  if (loading) {
    return (
      <div className="flex justify-center items-center min-h-[50vh]">
        <div className="animate-spin rounded-full h-12 w-12 border-t-2 border-b-2 border-primary"></div>
      </div>
    );
  }

  if (error || !conference) {
    return (
      <div className="flex flex-col items-center justify-center min-h-[50vh] gap-4">
        <p className="text-red-500 font-medium text-lg">{error || 'Không tìm thấy hội nghị.'}</p>
        <button
          onClick={() => onNavigate?.('conference-list')}
          className="px-4 py-2 bg-primary text-white rounded-lg hover:bg-primary-hover transition-colors"
        >
          Quay lại danh sách
        </button>
      </div>
    );
  }

  return (
    <div className="w-full bg-background-light dark:bg-background-dark py-12 px-5 md:px-10 flex justify-center">
      <div className="w-full max-w-[1200px] flex flex-col md:flex-row gap-8">
        {/* Main Content */}
        <div className="flex-1 bg-white dark:bg-card-dark rounded-xl border border-border-light dark:border-border-dark p-8 shadow-sm h-fit">
          <div className="mb-6">
            <div className="flex items-center gap-3 mb-2">
              <span className="px-3 py-1 rounded-full bg-blue-100 dark:bg-blue-900/30 text-primary text-sm font-bold tracking-wider">
                {conference.acronym}
              </span>
              <span className={`px-3 py-1 rounded-full text-xs font-bold uppercase ${conference.status === 'Open' ? 'bg-green-100 text-green-700' :
                conference.status === 'Closed' ? 'bg-red-100 text-red-700' : 'bg-gray-100 text-gray-700'
                }`}>
                {conference.status}
              </span>
            </div>
            <h1 className="text-3xl md:text-4xl font-bold text-text-main-light dark:text-text-main-dark mb-4">
              {conference.name}
            </h1>
          </div>

          <div className="prose dark:prose-invert max-w-none text-text-main-light dark:text-text-main-dark leading-relaxed">
            <h3 className="text-xl font-bold mb-2">Giới thiệu</h3>
            <div className="whitespace-pre-line text-justify mb-6">
              {conference.description || 'Chưa có mô tả cho hội nghị này.'}
            </div>

            {conference.topics && conference.topics.length > 0 && (
              <>
                <h3 className="text-xl font-bold mb-3">Chủ đề (Topics)</h3>
                <ul className="grid grid-cols-1 md:grid-cols-2 gap-2 list-disc pl-5">
                  {conference.topics.map((topic) => (
                    <li key={topic.id} className="text-text-sec-light dark:text-text-sec-dark">
                      <span className="font-medium text-text-main-light dark:text-text-main-dark">{topic.name}</span>
                      {topic.description && <span className="text-sm"> - {topic.description}</span>}
                    </li>
                  ))}
                </ul>
              </>
            )}
          </div>
        </div>

        {/* Sidebar */}
        <div className="w-full md:w-[350px] flex flex-col gap-6">
          <div className="bg-white dark:bg-card-dark rounded-xl border border-border-light dark:border-border-dark p-6 shadow-sm">
            <h3 className="text-lg font-bold text-primary border-b border-border-light dark:border-border-dark pb-3 mb-4 flex items-center gap-2">
              <span className="material-symbols-outlined">event</span>
              Các mốc thời gian
            </h3>

            <div className="space-y-4">
              <div className="flex flex-col gap-1">
                <span className="text-xs font-medium text-text-sec-light dark:text-text-sec-dark uppercase">Ngày bắt đầu</span>
                <p className="font-medium text-text-main-light dark:text-text-main-dark flex items-center gap-2">
                  <span className="material-symbols-outlined text-primary text-[20px]">play_circle</span>
                  {new Date(conference.startDate).toLocaleDateString('vi-VN')}
                </p>
              </div>

              <div className="flex flex-col gap-1">
                <span className="text-xs font-medium text-text-sec-light dark:text-text-sec-dark uppercase">Ngày kết thúc</span>
                <p className="font-medium text-text-main-light dark:text-text-main-dark flex items-center gap-2">
                  <span className="material-symbols-outlined text-red-500 text-[20px]">stop_circle</span>
                  {new Date(conference.endDate).toLocaleDateString('vi-VN')}
                </p>
              </div>

              <div className="flex flex-col gap-1 p-3 bg-blue-50 dark:bg-blue-900/10 rounded-lg border border-blue-100 dark:border-blue-900/30">
                <span className="text-xs font-bold text-blue-800 dark:text-blue-300 uppercase">Hạn nộp bài</span>
                <p className="font-bold text-blue-700 dark:text-blue-400 flex items-center gap-2 text-lg">
                  <span className="material-symbols-outlined text-[20px]">timer</span>
                  {new Date(conference.submissionDeadline).toLocaleDateString('vi-VN')}
                </p>
              </div>
            </div>

            {conference.status === 'Open' && (
              <div className="mt-6 pt-4 border-t border-border-light dark:border-border-dark">
                <button
                  onClick={() => onNavigate?.('call-for-papers')}
                  className="w-full py-3 rounded-lg font-bold text-white transition-all flex items-center justify-center gap-2 bg-primary hover:bg-primary-hover shadow-md hover:shadow-lg"
                >
                  <span className="material-symbols-outlined">upload_file</span>
                  Nộp bài ngay
                </button>
              </div>
            )}
          </div>

          <div className="bg-white dark:bg-card-dark rounded-xl border border-border-light dark:border-border-dark p-6 shadow-sm">
            <h3 className="text-lg font-bold text-text-main-light dark:text-text-main-dark border-b border-border-light dark:border-border-dark pb-3 mb-4 flex items-center gap-2">
              <span className="material-symbols-outlined">location_on</span>
              Địa điểm
            </h3>
            <p className="text-text-main-light dark:text-text-main-dark mb-4">
              {conference.location || "Online"}
            </p>

          </div>
        </div>
      </div>
    </div>
  );
};
