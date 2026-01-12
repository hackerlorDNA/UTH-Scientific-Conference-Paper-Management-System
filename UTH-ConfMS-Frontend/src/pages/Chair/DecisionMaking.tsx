import React from 'react';
import { ViewState } from '../../App';

export const DecisionMaking: React.FC<{ onNavigate: (view: ViewState) => void }> = ({ onNavigate }) => {
  const papers = [
    { id: 'P01', title: 'AI in Healthcare', score: 8.5 },
    { id: 'P02', title: 'IoT Security', score: 4.2 },
  ];

  return (
    <div className="p-8">
      <div className="flex justify-between items-center mb-8">
        <h2 className="text-2xl font-bold">Danh sách Ra quyết định (3.3.5)</h2>
        <button onClick={() => onNavigate('chair-dashboard')} className="text-primary flex items-center gap-1">Quay lại Dashboard</button>
      </div>
      <div className="bg-white dark:bg-gray-800 rounded-xl shadow overflow-hidden">
        <table className="w-full text-left border-collapse">
          <thead className="bg-gray-50 dark:bg-gray-700">
            <tr>
              <th className="p-4 border-b">Mã bài</th>
              <th className="p-4 border-b">Tiêu đề</th>
              <th className="p-4 border-b">Điểm TB</th>
              <th className="p-4 border-b text-right">Thao tác</th>
            </tr>
          </thead>
          <tbody>
            {papers.map(p => (
              <tr key={p.id} className="hover:bg-gray-50 dark:hover:bg-gray-700/50">
                <td className="p-4 border-b font-mono">#{p.id}</td>
                <td className="p-4 border-b">{p.title}</td>
                <td className="p-4 border-b font-bold text-blue-600">{p.score}</td>
                <td className="p-4 border-b text-right space-x-2">
                  <button className="px-3 py-1 bg-green-100 text-green-700 rounded">Chấp nhận</button>
                  <button className="px-3 py-1 bg-red-100 text-red-700 rounded">Từ chối</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default DecisionMaking;