import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { GoogleOAuthProvider } from '@react-oauth/google'
import { BrowserRouter } from 'react-router-dom'

createRoot(document.getElementById('root')!).render(
  <GoogleOAuthProvider clientId="887178122058-kuopeve8qvph8fuoh1a7jpgtem9npsmc.apps.googleusercontent.com">
    <BrowserRouter>
      <App />
    </BrowserRouter>
  </GoogleOAuthProvider>,
)
