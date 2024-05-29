import React from 'react'
import ReactDOM from 'react-dom/client'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'

import './styles/index.css'
import App from './App.tsx'
import AuthContextWrapper from './context/AuthContextWrapper.tsx'

const queryClient = new QueryClient();

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <QueryClientProvider client={queryClient}>
      <AuthContextWrapper>
        <App />
      </AuthContextWrapper>
    </QueryClientProvider>
  </React.StrictMode>
)
