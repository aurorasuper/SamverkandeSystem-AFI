import React, { useEffect } from 'react'
import { useState } from 'react'
import axios from 'axios'
import {BiEdit} from 'react-icons/bi'

function EditSubscriber({user}) {
    const [sub, setSub] = useState(user)
    const [errorMessages, setErrorMessages]=useState({})
    const [loading, setLoading] = useState(true);
    const numberPattern = /[^0-9]/;

    useEffect(()=>{
        console.log(sub);
        delete sub.tblAds;
        setSub(sub);
        setLoading(false);
    },[loading])

    /**
     * Check so error messages contain no errors
     */
    const noErrors = Object.values(errorMessages).every(
        value => value === ""
    );


    /**
     * Hanterar ändringar i form input och uppdaterar state. Validerar även fält som bara får ta emot nummer. 
     * @param {*} e | ändrat input
     */
    const handleChange = (e) =>{
        const {name, value} = e.target;
        //tomt fält
        if(value === ""){
            setErrorMessages({...errorMessages,[name]:"Fyll i fältet."})
        }else{
            setErrorMessages({...errorMessages,[name]:""});
        // nummertest
            if(name === "ownPhone" || name==="ownDeliveryZip"){
                if(numberPattern.test(value)){
                    setErrorMessages({...errorMessages,[name]:"Bara nummer är tillåtna."})
                }else{
                    setErrorMessages({...errorMessages,[name]:""})
                }
            }
            
        }
        setSub({...sub, [name]:value});
    }

    /**
     * Hantera inskick av formulär. Gör ett put request till api. 
     */
    const handleSubmit = (e) => {
        e.preventDefault();
        if(noErrors){
            axios.put('https://localhost:7015/api/AdOwner/'+sub.ownId, sub)
        .then(res => {
            console.log(res)
        }).catch(error => {
            console.log(error.response)
            if(error.response.status === 404)
            setErrorMessages({...errorMessages, getError:"Ooops. Prenumerationsnummret finns inte i databasen."});
        })
        }
    }

    const editBtn = () =>{

        let inputs = document.getElementsByTagName('input')
        for(let i = 1; i<inputs.length; i++){
            let j = inputs[i];
            j.readOnly = false;
        }
        document.getElementById('submitBtn').innerHTML = 'Spara';
       
    }

  return (
    <div>
        <h2>Kontrollera dina uppgifter</h2>
        {!loading &&
        <form onSubmit={handleSubmit} className='flex flex-col space-y-4'>
            <div>
                <label>Prenumerationsnummer</label>
                <input type="text" name="ownName" value={sub.ownSubId} readOnly className='read'></input>
                
            </div>
            <h3>Personinformation</h3>
             <div>
            
                <label>Namn</label>
                <div className='flex flex-row'>
                    <input type="text" name="ownName" value={sub.ownName} readOnly onChange={handleChange}></input>
                    
                   
                </div>
                {errorMessages.ownName !== "" ? <p className="text-red-500">{errorMessages.ownName}</p> : null}
                </div>
            <div>
                <label>Telefonnummer</label>
                <div className='flex flex-row'>
                    <input type="text" name="ownPhone" value={sub.ownPhone} readOnly onChange={handleChange}></input>
                    
                    
                </div>
                {errorMessages.ownPhone !== "" ? <p className="text-red-500">{errorMessages.ownPhone}</p> : null}
            </div>
            <h3>Leveransadress</h3>
            <div>
                <label>Adress</label>
                <div className='flex flex-row'>
                <input type="text" name="ownDeliveryAdress" value={sub.ownDeliveryAdress} readOnly onChange={handleChange}></input>
              
                </div>
                {errorMessages.ownDeliveryAdress !== "" ? <p className="text-red-500">{errorMessages.ownDeliveryAdress}</p> : null}
            </div>
            <div className="flex flex-row space-x-3">
                <div className="w-1/2 shrink">
                    <label>Postnummer</label>
                    <div className='flex flex-row'>
                    <input type="text" name="ownDeliveryZip" value={sub.ownDeliveryZip} readOnly onChange={handleChange}></input>
                    
                    </div>
                    {errorMessages.ownDeliveryZip !== "" ? <p className="text-red-500">{errorMessages.ownDeliveryZip}</p> : null}
                </div>
                <div className="w-1/2 shrink">
                    <label>Ort</label>
                    <div className='flex flex-row'>
                    <input type="text" name='ownDeliveryCounty' value={sub.ownDeliveryCounty} readOnly onChange={handleChange}></input>
                
                    </div>
                    {errorMessages.ownDeliveryCounty !== "" ? <p className="text-red-500">{errorMessages.ownDeliveryCounty}</p> : null}
                </div>
            </div>
            {errorMessages.getError !== "" ? <p className="text-red-500 text-center">{errorMessages.getError}</p> : null}
            <div className='flex flex-row space-x-4'>
            <button className=' bg-red-200' onClick={editBtn}>Ändra</button>
            <button type="submit" className='next-btn' id='submitBtn'>Bekräfta</button>
            </div>
            
        </form>
        }
        
    </div>
  )
}

export default EditSubscriber