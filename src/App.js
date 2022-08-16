import './App.css';
import React from 'react';
import { useState , useEffect, useRef} from 'react';
import axios from 'axios';
import Ad from './components/Ad.js'
import { Navigate, useNavigate } from 'react-router-dom';

function App() {
  const [ads, setAds] = useState([]);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    axios.get('https://localhost:7015/api/Ads')
            .then(res => {
                setAds(res.data)
            }).catch(err => {
                return err;
            })
            setLoading(false);
  }, [])
  
  const createAd = () =>{
    navigate("/CreateAd");
  }
  
  return (
    
    <div className="App">
      <header className="App-header">
       <h1 className='text-2xl'>Annonser</h1>
       <button className='block hover:bg-blue-500 hover:text-white p-2 rounded-lg text-base text-blue-500 border-2 border-blue-500' onClick={createAd}>Skapa annons</button>
      </header>
      
      <div className="main">
      {!loading ? 
        <div>
          {ads.map(ad => 
          <Ad key={ad.adId} ad={ad}/>)}
          </div>:
      <p>loading...</p>}
        
      </div>


    </div>
  );
}

export default App;
