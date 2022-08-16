import React from 'react';
import axios from 'axios';
import { useState } from 'react';

function GetSubscriber({passUser}) {
    const [subId, setSub] = useState(null);
    const [err, setErr] = useState({});
    const numberPattern = /[^0-9]/;


const handleSubmit = (e) =>{
    e.preventDefault();
    setErr({...err, getErr:""});
    setErr({...err, validationErr:""});
    if(subId!== null || subId !== ""){
        axios.get('https://localhost:7015/api/AdOwner/Subscriber/'+subId)
        .then(res => {
            passUser(res.data);
        }).catch(error => {
            if(error.response.status === 404)
            setErr({...err, getErr:"Ooops. Prenumerationsnummret finns inte i databasen."});
        })
    }else{
        setErr({...err, validationErr:"Vänligen fyll i alla fält korrekt."});
    }

    console.log(subId)
}

const handleChange = (e) =>{
    const value = e.target.value;
    setSub(value);
    if(subId !== null && subId.length >= 9){
        document.getElementById("submit-id").disabled=false;
    }
    if(numberPattern.test(value)){
        setErr({...err,subId:"Bara nummer är tillåtna"});
    }else{
        setErr({...err,subId:""});
    }
}


  return (
    <div>
        <form className="flex flex-col space-y-4" onSubmit={handleSubmit}>
                {err.getErr !== ""? <p className='text-red-500 text-center'>{err.getErr}</p> : null}
                <div>
                    <label>Prenumerationsnummer</label>
                    <input type="text" name="subId" onChange={handleChange}/>
                    {err.subId !== ""? <p className='text-red-500'>{err.subId}</p> : null}
                </div>
                
                    {err.validationErr !== ""? <p className='text-red-500 text-center'>{err.validationErr}</p> : null}
                    <button id="submit-id" type="submit" disabled className='next-btn'>Nästa</button>
                
               
        </form>


    </div>
  )
}

export default GetSubscriber