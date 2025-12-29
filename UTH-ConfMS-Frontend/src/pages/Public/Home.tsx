import React from 'react';
import { ViewState } from '../../App';
import { Hero } from './Hero';
import { ProcessSection } from './ProcessSection';
import { ImportantDates } from './ImportantDates';

interface HomeProps {
  onNavigate?: (view: ViewState) => void;
}

export const Home: React.FC<HomeProps> = ({ onNavigate }) => {
  const handleSubmitPaper = () => {
    if (onNavigate) {
      onNavigate('submit-paper');
    }
  };

  const handleViewCallForPapers = () => {
    if (onNavigate) {
      onNavigate('call-for-papers');
    }
  };

  return (
    <>
      <Hero 
        onSubmitPaper={handleSubmitPaper} 
        onViewCallForPapers={handleViewCallForPapers} 
      />
      <ImportantDates />
      <ProcessSection />
    </>
  );
};
