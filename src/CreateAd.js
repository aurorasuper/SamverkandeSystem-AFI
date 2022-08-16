import React, { useEffect } from 'react'
import { useState} from 'react';
import IsSubscriber from './components/IsSubscriber';
import GetSubscriber from './components/GetSubscriber';
import PostCompany from './components/PostCompany';
import { useNavigate } from 'react-router-dom';
import EditSubscriber from './components/EditSubscriber';

export default function CreateAd() {
    const [isSub, setSub] = useState(null);
    const [user, setUser] = useState({})
    const [step,setStep] = useState(0);
    const navigate = useNavigate();

    const cancel = () =>{
      navigate("/");
    }
    
    const handleCallback = (childData)=>{
        setSub(childData);
        console.log(childData)
        if(childData){
          setStep(1)
        }else{
          setStep(2)
        }

    }

    useEffect(()=>{
      if(step === 0){
        document.getElementById("back-btn").disabled=true;
      }else{
        document.getElementById("back-btn").disabled=false;
      }
    },[step])


    const handleBackBtn = () =>{
     
      switch(step){
        case 1: 
          setStep(0);
        break;
        case 2: 
          setStep(0);
        break;
        case 3: setStep(1);
        break;
        case 4: if(isSub){
          setStep(3)
        }else{setStep(2)}
        break;
      }
    }
    const userModel = (childData) => {
        setUser(childData);
        if(isSub){
          setStep(3);
        }else{
          setStep(4);
        }
    }

    const RenderSwitch = () =>{

        switch(step){
          case 0: return <IsSubscriber status={isSub} setStatus={handleCallback}/>;
          case 1: return <GetSubscriber passUser={userModel}/>;
          case 2: return <PostCompany passUser={userModel}/>;
          case 3: return <EditSubscriber user={user} setUser={setUser}/>;
          //case 3 user is subscriber - check subscriber information from get
          //case 4 create ad  
      }
    }

  return (
    <div>
    <div className="App">
      <header className="App-header">
       <h1>Annonser</h1>
      </header>
      <div className="main">
            <h2>Skapa en annons</h2>
            <RenderSwitch/>
            
        <button id="back-btn" className='back-btn' onClick={handleBackBtn} >Tillbaka</button>
        <button className="my-3 text-gray-500 text-sm underline decoration-dotted hover:text-blue-400 hover:cursor-pointer"onClick={cancel}>Avbryt</button>
      </div>
        

    </div>
    </div>
  )
}
