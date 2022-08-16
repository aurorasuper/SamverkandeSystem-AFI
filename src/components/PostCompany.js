import React, { useEffect, useState } from 'react';
import axios from 'axios';


function PostCompany(passUser) {
    const [company, setCompany] = useState({OwnIsSub: false, OwnCompanyOrgNr:"", OwnName:"", OwnPhone:"", OwnDeliveryAdress:"", OwnDeliveryZip:"", OwnDeliveryCounty:"",
        OwnBillingAdress:"", OwnBillingZip:"", OwnBillingCounty:""});
    const [response, setResponse] = useState({});
    const numberPattern = /[^0-9]/;
    const [errorMessages, setErrorMessages] = useState({OwnCompanyOrgNr:"", OwnPhone:"",OwnDeliveryZip:"", OwnBillingZip:""});

    
    const handleSubmit = (e) =>{
        e.preventDefault();
        
        axios.post('https://localhost:7015/api/AdOwner/Company',company)
        .then(res => {
            setResponse(res);
            console.log(res.data)
            passUser(res.data);
        }).catch(err => {
            console.log("Err" + err)
        })
    }

    const validate = (o) =>{
        //zip =5 
        if(o.OwnBillingZip.length !== 5){
            setErrorMessages({...errorMessages,OwnBillingZip:"Postnummret måste vara 5 karaktärer långt"})
        }else{
            setErrorMessages({...errorMessages,OwnBillingZip:""})
        }

        if(o.OwnDeliveryZip.length !== 5){
            setErrorMessages({...errorMessages,OwnDeliveryZip:"Postnummret måste vara 5 karaktärer långt"})
        }else{
            setErrorMessages({...errorMessages,OwnDeliveryZip:""})
        }

        if(o.OwnCompanyOrgNr.length !== 10){
            setErrorMessages({...errorMessages,OwnCompanyOrgNr:"Organisationsnummret måste vara 10 karaktärer långt"})
        }else{
            setErrorMessages({...errorMessages,OwnCompanyOrgNr:""})
        }

        if(o.OwnPhone.length < 10){
            
                setErrorMessages({...errorMessages,OwnPhone:"Telefonnummer måste vara minst 10 karaktärer långt"})
            }else{
                setErrorMessages({...errorMessages,OwnPhone:""})
            }
        

    }

    const handleChange = (e) =>{
        const {name, value} = e.target;

        // nummertest
        if(name === "OwnCompanyOrgNr" || name === "OwnPhone" || name==="OwnDeliveryZip" || name==="OwnBillingZip"){
            if(numberPattern.test(value)){
                setErrorMessages({...errorMessages,[name]:"Bara nummer är tillåtna."})
            }else{
                setErrorMessages({...errorMessages,[name]:""})
            }
        }

        //tomt fält
        if(value === ""){
            setErrorMessages({...errorMessages,[name]:"Fyll i fältet."})
        }else{
            setErrorMessages({...errorMessages,[name]:""});
        }
        setCompany({...company, [name]:value});
    }
  return (
    <div>
        <p>Företagsinformation</p>
        <form onSubmit={handleSubmit} className="flex flex-col flex-auto space-y-5">
            <div>
                <label>Organisationsnummer</label>
                <input type="text" name="OwnCompanyOrgNr" onChange={handleChange}></input>
                {errorMessages.OwnCompanyOrgNr !== "" ? <p className="text-red-500">{errorMessages.OwnCompanyOrgNr}</p> : null}
            </div>
            <div>
                <label>Namn</label>
                <input type="text" name="OwnName" onChange={handleChange}></input>
                {errorMessages.OwnName !== "" ? <p className="text-red-500">{errorMessages.OwnName}</p> : null}
            </div>
            <div>
                <label>Telefonnummer</label>
                <input type="text" name="OwnPhone" onChange={handleChange}></input>
                {errorMessages.OwnPhone !== "" ? <p className="text-red-500">{errorMessages.OwnPhone}</p> : null}
            </div>
            <p>Leveransadress</p>
            <div>
                <label>Adress</label>
                <input type="text" name="OwnDeliveryAdress" onChange={handleChange}></input>
                {errorMessages.OwnDeliveryAdress !== "" ? <p className="text-red-500">{errorMessages.OwnDeliveryAdress}</p> : null}
            </div>
            <div className="flex flex-row space-x-3">
                <div className="w-1/2 shrink">
                    <label>Postnummer</label>
                    <input type="text" name="OwnDeliveryZip" onChange={handleChange}></input>
                    {errorMessages.OwnDeliveryZip !== "" ? <p className="text-red-500">{errorMessages.OwnDeliveryZip}</p> : null}
                </div>
                <div className="w-1/2 shrink">
                    <label>Ort</label>
                    <input type="text" name='OwnDeliveryCounty' onChange={handleChange}></input>
                    {errorMessages.OwnDeliveryCounty !== "" ? <p className="text-red-500">{errorMessages.OwnDeliveryCounty}</p> : null}
                </div>
            </div>
            
            <p>Faktureringsadress</p>
            <div>
                <label>Adress</label>
                <input type="text" name='OwnBillingAdress' onChange={handleChange}></input>
                {errorMessages.OwnBillingAdress !== "" ? <p className="text-red-500">{errorMessages.OwnBillingAdress}</p> : null}
            </div>
            <div className="flex flex-row space-x-3">
                <div className="w-1/2 shrink">
                    <label>Postnummer</label>
                    <input type="text" name='OwnBillingZip' onChange={handleChange}></input>
                    {errorMessages.OwnBillingZip !== "" ? <p className="text-red-500">{errorMessages.OwnBillingZip}</p> : null}
                </div>
                <div className="w-1/2 shrink">
                    <label>Ort</label>
                    <input type="text" nem="OwnBillingCounty" onChange={handleChange}></input>
                    {errorMessages.OwnBillingCounty !== "" ? <p className="text-red-500">{errorMessages.OwnBillingCounty}</p> : null}
                </div>
            </div>
        </form>
        <button type="submit">Ok</button>

    </div>
  )
}

export default PostCompany