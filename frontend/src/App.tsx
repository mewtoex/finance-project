import { DashboardView } from "./components/dashboard/DashboardView";
import { Toaster } from "sonner"; 

function App() {
  return (
    <div className="min-h-screen bg-background font-sans antialiased">
      <DashboardView />
      
      <Toaster richColors position="top-right" />
    </div>
  );
}

export default App;