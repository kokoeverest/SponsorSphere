// src/App.tsx
import React, { useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './styles/App.css';
import AthleteList from './components/AthleteList';
import AthleteDetail from './components/AthleteDetail';
import Header from './components/Header';
import LoginForm from './components/forms/LoginForm';
import WelcomePage from './components/WelcomePage';
import RegisterChoice from './components/RegisterChoice';
import RegisterAthlete from './components/forms/RegisterAthleteForm';
// import RegisterCompany from './components/forms/RegisterSponsorCompanyForm';
// import RegisterIndividual from './components/forms/RegisterSponsorIndividualForm';


const App: React.FC = () => {
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(!!localStorage.getItem('token'));

  return (
    <Router>
      <Header isLoggedIn={isLoggedIn} setIsLoggedIn={setIsLoggedIn} />
      <div className="main-content">
        <Routes>
          <Route path="/" element={<WelcomePage />} />
          <Route path="/register" element={<RegisterChoice />} />
          <Route path="/register/athlete" element={<RegisterAthlete />} />
          {/* <Route path="/register/company" element={<RegisterCompany />} />
          <Route path="/register/individual" element={<RegisterIndividual />} /> */}
          <Route path="/athletes" element={<AthleteList />} />
          <Route path="/athletes/:id" element={<AthleteDetail />} />
          <Route path="/login" element={<LoginForm />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;