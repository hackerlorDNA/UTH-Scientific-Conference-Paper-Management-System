  import React, { useState } from 'react';
  import { Navbar } from './components/Navbar';
  import { Home } from './pages/Public/Home';
  import { Login } from './pages/Auth/Login';
  import { Register } from './pages/Auth/Register';
  import { ForgotPassword } from './pages/Auth/ForgotPassword';
  import { Footer } from './components/Footer';
  import { ConferenceDetails } from './components/ConferenceDetails';
  import { CallForPapers } from './components/CallForPapers';
  import { Program } from './components/Program';
  import { AuthorDashboard } from './pages/Author/Dashboard';
  import { SubmitPaper } from './pages/Author/SubmitPaper';
  import { ReviewerDashboard } from './pages/Reviewer/Dashboard';
  // 3.3.4: Thêm giao diện Phản biện bài báo
  import ReviewForm from './pages/Reviewer/ReviewForm'; 
  import { ChairDashboard } from './pages/Chair/Dashboard';
  // 3.3.5: Thêm giao diện Đưa ra quyết định
  import DecisionMaking from './pages/Chair/DecisionMaking'; 
  import { AdminDashboard } from './pages/Admin/AdminDashboard';
  import { UserManagement } from './pages/Admin/UserManagement';
  import { SystemConfig } from './pages/Admin/SystemConfig';
  // 3.3.6: Thêm giao diện Quản trị hội nghị (thuộc Quản trị hệ thống)
  import ConferenceManagement from './pages/Admin/ConferenceManagement'; 
  import { DecisionNotification } from './components/DecisionNotification';
  import { Profile } from './components/Profile';
  import { AuthProvider } from './contexts/AuthContext';

  // Cập nhật ViewState để bao gồm các màn hình mới
  export type ViewState = 
    | 'home' 
    | 'login' 
    | 'register' 
    | 'forgot-password'
    | 'conference-details' 
    | 'call-for-papers' 
    | 'program' 
    | 'author-dashboard' 
    | 'submit-paper' 
    | 'reviewer-dashboard' 
    | 'reviewer-form'        // Mới (3.3.4)
    | 'chair-dashboard' 
    | 'chair-decisions'      // Mới (3.3.5)
    | 'admin-dashboard'
    | 'admin-users'
    | 'admin-system-config'
    | 'admin-conferences'    // Mới (3.3.6)
    | 'decision' 
    | 'profile';

  const App: React.FC = () => {
    // Bạn có thể truyền paperId qua một state phụ nếu cần thiết cho reviewer-form
    const [currentView, setCurrentView] = useState<ViewState>('home');
    const [selectedPaperId, setSelectedPaperId] = useState<string | null>(null);

    const renderContent = () => {
      switch (currentView) {
        case 'home': return <Home />;
        case 'login': return <Login onNavigate={setCurrentView} />;
        case 'register': return <Register onNavigate={setCurrentView} />;
        case 'forgot-password': return <ForgotPassword onNavigate={setCurrentView} />;
        case 'conference-details': return <ConferenceDetails />;
        case 'call-for-papers': return <CallForPapers onNavigate={setCurrentView} />;
        case 'program': return <Program />;
        case 'author-dashboard': return <AuthorDashboard onNavigate={setCurrentView} />;
        case 'submit-paper': return <SubmitPaper onNavigate={setCurrentView} />;
        
        // 3.3.4: Reviewer
        case 'reviewer-dashboard': return <ReviewerDashboard onNavigate={(view, id) => {
          setCurrentView(view);
          if (id) setSelectedPaperId(id);
        }} />;
        case 'reviewer-form': return <ReviewForm paperId={selectedPaperId || ''} onNavigate={setCurrentView} />;
        
        // 3.3.5: Chair
        case 'chair-dashboard': return <ChairDashboard onNavigate={setCurrentView} />;
        case 'chair-decisions': return <DecisionMaking onNavigate={setCurrentView} />;
        
        // 3.3.6: Admin
        case 'admin-dashboard': return <AdminDashboard onNavigate={setCurrentView} />;
        case 'admin-users': return <UserManagement onNavigate={setCurrentView} />;
        case 'admin-system-config': return <SystemConfig onNavigate={setCurrentView} />;
        case 'admin-conferences': return <ConferenceManagement onNavigate={setCurrentView} />;
        
        case 'decision': return <DecisionNotification />;
        case 'profile': return <Profile />;
        default: return <Home />;
      }
    };

    return (
      <AuthProvider>
        <div className="relative flex h-auto min-h-screen w-full flex-col overflow-x-hidden bg-background-light dark:bg-background-dark transition-colors duration-200">
          <Navbar onNavigate={setCurrentView} currentView={currentView} />
          <main className="flex flex-col grow">
            {renderContent()}
          </main>
          <Footer onNavigate={setCurrentView} />
        </div>
      </AuthProvider>
    );
  };

  export default App;