
import React, { useState, useEffect } from 'react';
import { PDFPreview } from '../../components/PDFPreview';
import { AIBadge } from '../../components/AIBadge';
import { ReviewForm } from '../../components/ReviewForm';
import { reviewerApi, ReviewerInvitationDto } from '../../services/reviewerApi';

export const ReviewerDashboard: React.FC = () => {
  const [selectedPaper, setSelectedPaper] = useState<number | null>(null);
    const [invitations, setInvitations] = useState<ReviewerInvitationDto[]>([]);
    const [showInvitations, setShowInvitations] = useState(false);

    const fetchInvitations = async () => {
        try {
            const resp = await reviewerApi.getMyInvitations();
            // apiClient returns ApiResponse wrapper; some endpoints return raw arrays in this project
            const data = (resp as any).data ?? (resp as any);
            setInvitations(data);
        } catch (err) {
            console.error('Failed to load invitations', err);
        }
    };

    useEffect(() => {
        // Load invitations once when dashboard mounts
        fetchInvitations();
    }, []);

  return (
    <div className="w-full bg-background-light dark:bg-background-dark py-8 px-5 md:px-10 flex justify-center">
        <div className="w-full max-w-[1200px] grid grid-cols-1 lg:grid-cols-3 gap-6">
            
            {/* Paper List */}
            <div className={`lg:col-span-${selectedPaper ? '1' : '3'} flex flex-col gap-4 transition-all`}>
                <div className="flex items-center justify-between">
                    <h1 className="text-2xl font-bold text-text-main-light dark:text-text-main-dark mb-2">Bài được phân công</h1>
                    <div className="flex items-center gap-3">
                        <button
                            onClick={() => { setShowInvitations(!showInvitations); if (!showInvitations) fetchInvitations(); }}
                            className="px-3 py-1 bg-primary hover:bg-primary-hover text-white font-medium rounded-md shadow-sm transition flex items-center gap-2"
                            aria-label="Lời mời phân công"
                        >
                            <span className="material-symbols-outlined">assignment</span>
                            Lời mời phân công {invitations.filter(i => i.status === 'Pending').length > 0 && (
                                <span className="ml-2 text-sm text-white bg-red-600 px-2 py-0.5 rounded">{invitations.filter(i => i.status === 'Pending').length}</span>
                            )}
                        </button>
                    </div>
                </div>

                {showInvitations && (
                    <div className="mb-4 p-4 bg-white dark:bg-card-dark border border-border-light rounded-lg">
                        <h3 className="font-semibold mb-2">Lời mời phân công của bạn</h3>
                        {invitations.length === 0 && <p className="text-sm text-text-sec-light">Không có lời mời nào.</p>}
                        {invitations.map(inv => (
                            <div key={inv.id} className="flex items-center justify-between p-2 border-b last:border-b-0">
                                <div>
                                    <div className="font-medium">{inv.fullName}</div>
                                    <div className="text-xs text-text-sec-light">{inv.email} • {new Date(inv.sentAt).toLocaleString()}</div>
                                </div>
                                <div className="flex items-center gap-2">
                                    {inv.status === 'Pending' ? (
                                        <>
                                            <button onClick={async () => { await reviewerApi.respondInvitation({ token: inv.token ?? '', isAccepted: true }); await fetchInvitations(); }} className="px-3 py-1 bg-green-500 text-white rounded">Chấp nhận</button>
                                            <button onClick={async () => { await reviewerApi.respondInvitation({ token: inv.token ?? '', isAccepted: false }); await fetchInvitations(); }} className="px-3 py-1 bg-red-500 text-white rounded">Từ chối</button>
                                        </>
                                    ) : (
                                        <span className="text-sm text-text-sec-light">{inv.status}</span>
                                    )}
                                </div>
                            </div>
                        ))}
                    </div>
                )}
                {[1, 2, 3].map((id) => (
                    <div 
                        key={id} 
                        onClick={() => setSelectedPaper(id)}
                        className={`bg-white dark:bg-card-dark p-5 rounded-xl border cursor-pointer transition-all hover:shadow-md ${selectedPaper === id ? 'border-primary ring-1 ring-primary' : 'border-border-light'}`}
                    >
                        <div className="flex justify-between items-start mb-2">
                            <span className="text-xs font-mono bg-gray-100 px-2 py-1 rounded">#{100 + id}</span>
                            <span className="text-xs font-bold text-red-500">Hạn: 25/06</span>
                        </div>
                        <h3 className="font-bold text-sm mb-2">Deep Learning Approaches for Traffic Flow Prediction in Smart Cities</h3>
                        <p className="text-xs text-text-sec-light line-clamp-2">This paper proposes a novel hybrid architecture combining CNN and LSTM to predict traffic flow with high accuracy...</p>
                        <div className="mt-3 flex items-center gap-2 flex-wrap">
                            <span className="text-xs px-2 py-1 bg-blue-50 text-blue-700 rounded-full font-medium">AI & Big Data</span>
                            <span className="text-xs px-2 py-1 bg-yellow-50 text-yellow-700 rounded-full font-medium">Chờ phản biện</span>
                            {id === 1 && <AIBadge label="High Match" size="sm" />}
                        </div>
                    </div>
                ))}
            </div>

            {/* Review Form (Visible when selected) */}
            {selectedPaper && (
                <div className="lg:col-span-2 bg-white dark:bg-card-dark rounded-xl border border-border-light shadow-lg flex flex-col h-[800px] overflow-hidden">
                    <div className="p-4 border-b border-border-light flex justify-between items-center bg-gray-50 dark:bg-gray-800">
                        <h3 className="font-bold">Đánh giá bài báo #{100 + selectedPaper}</h3>
                        <button onClick={() => setSelectedPaper(null)} className="text-gray-400 hover:text-gray-600 material-symbols-outlined">close</button>
                    </div>
                    <div className="flex-1 overflow-y-auto p-6">
                        <div className="flex flex-col gap-6">
                            
                            {/* PDF Preview Integration */}
                            <div className="h-64 w-full">
                                <PDFPreview fileName={`paper_${100 + selectedPaper}_v1.pdf`} />
                            </div>

                            {/* Gọi Component ReviewForm tại đây */}
                            <ReviewForm paperId={selectedPaper} />
                        </div>
                    </div>
                </div>
            )}

        </div>
    </div>
  );
};