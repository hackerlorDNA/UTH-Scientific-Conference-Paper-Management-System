
import React, { useState } from 'react';
import { ViewState } from '../../App';
import { UserRole } from '../../contexts/AuthContext';

interface UserManagementProps {
  onNavigate: (view: ViewState) => void;
}

interface UserData {
  id: string;
  name: string;
  email: string;
  role: UserRole;
  status: 'active' | 'inactive';
  joinedDate: string;
}

const MOCK_USERS: UserData[] = [
  { id: '1', name: 'Nguyễn Văn A', email: 'vana@uth.edu.vn', role: 'author', status: 'active', joinedDate: '12/01/2024' },
  { id: '2', name: 'Trần Thị B', email: 'thib@uth.edu.vn', role: 'reviewer', status: 'active', joinedDate: '15/02/2024' },
  { id: '3', name: 'Lê Văn C', email: 'vanc@uth.edu.vn', role: 'chair', status: 'active', joinedDate: '20/02/2024' },
  { id: '4', name: 'Admin Root', email: 'admin@uth.edu.vn', role: 'admin', status: 'active', joinedDate: '01/01/2024' },
  { id: '5', name: 'Phạm Minh D', email: 'minhd@gmail.com', role: 'author', status: 'inactive', joinedDate: '05/03/2024' },
];

export const UserManagement: React.FC<UserManagementProps> = ({ onNavigate }) => {
  const [users, setUsers] = useState<UserData[]>(MOCK_USERS);
  const [search, setSearch] = useState('');
  const [showModal, setShowModal] = useState(false);
  const [editingUser, setEditingUser] = useState<UserData | null>(null);

  // Form State
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    role: 'author' as UserRole,
    status: 'active' as 'active' | 'inactive'
  });

  const filteredUsers = users.filter(u => 
    u.name.toLowerCase().includes(search.toLowerCase()) || 
    u.email.toLowerCase().includes(search.toLowerCase())
  );

  const handleOpenAdd = () => {
    setEditingUser(null);
    setFormData({ name: '', email: '', role: 'author', status: 'active' });
    setShowModal(true);
  };

  const handleOpenEdit = (user: UserData) => {
    setEditingUser(user);
    setFormData({ name: user.name, email: user.email, role: user.role, status: user.status });
    setShowModal(true);
  };

  const handleDelete = (id: string) => {
    if (window.confirm('Bạn có chắc chắn muốn xóa người dùng này?')) {
      setUsers(users.filter(u => u.id !== id));
    }
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (editingUser) {
      setUsers(users.map(u => u.id === editingUser.id ? { ...u, ...formData } : u));
    } else {
      const newUser: UserData = {
        id: Math.random().toString(36).substr(2, 9),
        ...formData,
        joinedDate: new Date().toLocaleDateString('vi-VN')
      };
      setUsers([newUser, ...users]);
    }
    setShowModal(false);
  };

  const getRoleBadge = (role: UserRole) => {
    const classes = {
      admin: 'bg-red-100 text-red-700 border-red-200',
      chair: 'bg-purple-100 text-purple-700 border-purple-200',
      reviewer: 'bg-blue-100 text-blue-700 border-blue-200',
      author: 'bg-green-100 text-green-700 border-green-200',
      public: 'bg-gray-100 text-gray-700 border-gray-200',
    };
    return <span className={`px-2 py-0.5 rounded-full text-[10px] font-bold border uppercase ${classes[role]}`}>{role}</span>;
  };

  return (
    <div className="w-full bg-background-light dark:bg-background-dark py-8 px-5 md:px-10 flex justify-center">
      <div className="w-full max-w-[1200px] flex flex-col gap-6">
        
        {/* Breadcrumbs & Header */}
        <div className="flex flex-col md:flex-row md:items-center justify-between gap-4">
          <div>
            <button 
              onClick={() => onNavigate('admin-dashboard')}
              className="text-primary text-sm font-medium flex items-center gap-1 hover:underline mb-2"
            >
              <span className="material-symbols-outlined text-sm">arrow_back</span> Quay lại Dashboard
            </button>
            <h1 className="text-2xl font-bold text-text-main-light dark:text-text-main-dark">Quản lý người dùng</h1>
          </div>
          <button 
            onClick={handleOpenAdd}
            className="bg-primary hover:bg-primary-hover text-white px-4 py-2 rounded-lg font-bold text-sm shadow-md flex items-center justify-center gap-2"
          >
            <span className="material-symbols-outlined">person_add</span> Thêm người dùng mới
          </button>
        </div>

        {/* Toolbar */}
        <div className="bg-white dark:bg-card-dark p-4 rounded-xl border border-border-light shadow-sm flex flex-col md:flex-row gap-4">
          <div className="relative flex-1">
            <span className="absolute left-3 top-1/2 -translate-y-1/2 text-text-sec-light">
              <span className="material-symbols-outlined text-[20px]">search</span>
            </span>
            <input 
              type="text" 
              placeholder="Tìm theo tên hoặc email..." 
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              className="w-full pl-10 pr-4 py-2 rounded-lg border border-border-light bg-background-light dark:bg-background-dark text-sm outline-none focus:ring-2 focus:ring-primary"
            />
          </div>
          <select className="px-4 py-2 rounded-lg border border-border-light bg-white dark:bg-background-dark text-sm outline-none">
            <option value="">Tất cả vai trò</option>
            <option value="admin">Admin</option>
            <option value="chair">Chair</option>
            <option value="reviewer">Reviewer</option>
            <option value="author">Author</option>
          </select>
        </div>

        {/* User Table */}
        <div className="bg-white dark:bg-card-dark rounded-xl border border-border-light shadow-sm overflow-hidden">
          <div className="overflow-x-auto">
            <table className="w-full text-left text-sm">
              <thead className="bg-gray-50 dark:bg-gray-800/50 border-b border-border-light text-xs uppercase font-bold text-text-sec-light">
                <tr>
                  <th className="px-6 py-4">Họ và tên</th>
                  <th className="px-6 py-4">Email</th>
                  <th className="px-6 py-4">Vai trò</th>
                  <th className="px-6 py-4">Trạng thái</th>
                  <th className="px-6 py-4 text-right">Thao tác</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-border-light">
                {filteredUsers.length > 0 ? filteredUsers.map(user => (
                  <tr key={user.id} className="hover:bg-gray-50 dark:hover:bg-gray-800/30 transition-colors">
                    <td className="px-6 py-4 font-medium text-text-main-light dark:text-text-main-dark">{user.name}</td>
                    <td className="px-6 py-4 text-text-sec-light">{user.email}</td>
                    <td className="px-6 py-4">{getRoleBadge(user.role)}</td>
                    <td className="px-6 py-4">
                      <span className={`flex items-center gap-1.5 ${user.status === 'active' ? 'text-green-600' : 'text-gray-400'}`}>
                        <span className={`w-2 h-2 rounded-full ${user.status === 'active' ? 'bg-green-600' : 'bg-gray-400'}`}></span>
                        {user.status === 'active' ? 'Hoạt động' : 'Khóa'}
                      </span>
                    </td>
                    <td className="px-6 py-4 text-right">
                      <div className="flex justify-end gap-2">
                        <button 
                          onClick={() => handleOpenEdit(user)}
                          className="p-1.5 text-blue-600 hover:bg-blue-50 rounded transition-colors" 
                          title="Chỉnh sửa / Phân quyền"
                        >
                          <span className="material-symbols-outlined text-[20px]">edit</span>
                        </button>
                        <button 
                          onClick={() => handleDelete(user.id)}
                          className="p-1.5 text-red-600 hover:bg-red-50 rounded transition-colors"
                          title="Xóa người dùng"
                        >
                          <span className="material-symbols-outlined text-[20px]">delete</span>
                        </button>
                      </div>
                    </td>
                  </tr>
                )) : (
                  <tr>
                    <td colSpan={5} className="px-6 py-12 text-center text-text-sec-light">Không tìm thấy người dùng nào phù hợp.</td>
                  </tr>
                )}
              </tbody>
            </table>
          </div>
          <div className="px-6 py-4 bg-gray-50 dark:bg-gray-800/50 border-t border-border-light flex justify-between items-center text-xs text-text-sec-light">
            <span>Hiển thị {filteredUsers.length} người dùng</span>
            <div className="flex gap-2">
              <button className="px-2 py-1 border rounded bg-white hover:bg-gray-50 disabled:opacity-50" disabled>Trước</button>
              <button className="px-2 py-1 border rounded bg-white hover:bg-gray-50 disabled:opacity-50" disabled>Sau</button>
            </div>
          </div>
        </div>

        {/* Modal Thêm/Sửa */}
        {showModal && (
          <div className="fixed inset-0 z-[100] flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm animate-in fade-in duration-200">
            <div className="w-full max-w-md bg-white dark:bg-card-dark rounded-2xl shadow-2xl overflow-hidden animate-in zoom-in duration-200">
              <div className="px-6 py-4 border-b border-border-light flex justify-between items-center">
                <h3 className="font-bold text-lg">{editingUser ? 'Chỉnh sửa người dùng' : 'Thêm người dùng mới'}</h3>
                <button onClick={() => setShowModal(false)} className="material-symbols-outlined text-gray-400 hover:text-gray-600">close</button>
              </div>
              <form onSubmit={handleSubmit} className="p-6 flex flex-col gap-4">
                <div className="flex flex-col gap-1.5">
                  <label className="text-sm font-bold">Họ và tên</label>
                  <input 
                    type="text" 
                    required 
                    value={formData.name}
                    onChange={e => setFormData({...formData, name: e.target.value})}
                    className="w-full px-3 py-2 border border-border-light rounded-lg bg-background-light dark:bg-background-dark text-sm outline-none focus:ring-2 focus:ring-primary"
                    placeholder="Nguyễn Văn A"
                  />
                </div>
                <div className="flex flex-col gap-1.5">
                  <label className="text-sm font-bold">Email</label>
                  <input 
                    type="email" 
                    required 
                    value={formData.email}
                    onChange={e => setFormData({...formData, email: e.target.value})}
                    className="w-full px-3 py-2 border border-border-light rounded-lg bg-background-light dark:bg-background-dark text-sm outline-none focus:ring-2 focus:ring-primary"
                    placeholder="example@uth.edu.vn"
                  />
                </div>
                <div className="grid grid-cols-2 gap-4">
                  <div className="flex flex-col gap-1.5">
                    <label className="text-sm font-bold">Vai trò (Phân quyền)</label>
                    <select 
                      value={formData.role}
                      onChange={e => setFormData({...formData, role: e.target.value as UserRole})}
                      className="w-full px-3 py-2 border border-border-light rounded-lg bg-background-light dark:bg-background-dark text-sm outline-none focus:ring-2 focus:ring-primary"
                    >
                      <option value="author">Author</option>
                      <option value="reviewer">Reviewer</option>
                      <option value="chair">Chair</option>
                      <option value="admin">Admin</option>
                    </select>
                  </div>
                  <div className="flex flex-col gap-1.5">
                    <label className="text-sm font-bold">Trạng thái</label>
                    <select 
                      value={formData.status}
                      onChange={e => setFormData({...formData, status: e.target.value as 'active' | 'inactive'})}
                      className="w-full px-3 py-2 border border-border-light rounded-lg bg-background-light dark:bg-background-dark text-sm outline-none focus:ring-2 focus:ring-primary"
                    >
                      <option value="active">Hoạt động</option>
                      <option value="inactive">Khóa tài khoản</option>
                    </select>
                  </div>
                </div>
                
                <div className="pt-4 flex gap-3">
                  <button 
                    type="button" 
                    onClick={() => setShowModal(false)}
                    className="flex-1 py-2 border border-border-light rounded-lg font-bold text-sm hover:bg-gray-50 transition-colors"
                  >
                    Hủy
                  </button>
                  <button 
                    type="submit"
                    className="flex-1 py-2 bg-primary hover:bg-primary-hover text-white font-bold text-sm rounded-lg shadow-md transition-all"
                  >
                    {editingUser ? 'Lưu thay đổi' : 'Tạo tài khoản'}
                  </button>
                </div>
              </form>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};