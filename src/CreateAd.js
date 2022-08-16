import React, { useEffect } from 'react'
import { useState, useRef} from 'react';
import IsSubscriber from './components/IsSubscriber';
import GetSubscriber from './components/GetSubscriber';
import PostCompany from './components/PostCompany';
import { useNavigate } from 'react-router-dom';

export default function CreateAd() {
    const isSubscriber = useRef(false);
    const [isSub, setSub] = useState(null);
    const [user, setUser] = useState({})


    
    const handleCallback = (childData)=>{
        setSub(childData);
        console.log(isSub)

    }

    

  return (
    <div>
    <div className="App">
      <header className="App-header">
       <h1 className='text-2xl'>Annonser</h1>
      </header>
      <div className="main">
            <h2>Skapa en annons</h2>
            <IsSubscriber status={isSub} setStatus={handleCallback}/>
            
            {isSub == "true" && <GetSubscriber/>}
            {isSub == "false" && <PostCompany passUser={setUser}/>}
        
          {user && JSON.stringify(user)}
        

        <button disabled={isSub == null}className='block hover:bg-slate-500 hover:text-white p-2 rounded-lg text-base text-slate-500 border-2 border-slate-500'>Avbryt</button>
        <button className='block hover:bg-blue-500 hover:text-white p-2 rounded-lg text-base text-blue-500 border-2 border-blue-500' >Nästa</button>
      </div>
        

    </div>
    </div>
  )
}
