import React, { useEffect, useState } from 'react';
import axios from 'axios';


function PostCompany({passUser}) {
    const [company, setCompany] = useState({OwnIsSub: false, OwnCompanyOrgNr:"", OwnName:"", OwnPhone:"", OwnDeliveryAdress:"", OwnDeliveryZip:"", OwnDeliveryCounty:"",
        OwnBillingAdress:"", OwnBillingZip:"", OwnBillingCounty:""});
    const [response, setResponse] = useState({});
    const numberPattern = /[^0-9]/;
    const [errorMessages, setErrorMessages] = useState({OwnCompanyOrgNr:"", OwnPhone:"",OwnDeliveryZip:"", OwnBillingZip:""});

    const noErrors = Object.values(errorMessages).every(
        value => value === ""
    );

    const hasValues = Object.values(company).every(
        value => value !== "" || typeof value === Boolean
    )
    const handleSubmit = (e) =>{
        
        e.preventDefault();
        validate(company);
        if(noErrors){
            axios.post('https://localhost:7015/api/AdOwner/Company',company)
            .then(res => {
                setResponse(res);
                console.log(res.data)
                passUser(res.data);
            }).catch(err => {
                console.log("Err" + err)
            })
        }else{
            setErrorMessages({...errorMessages,submitError:"Vänligen fyll i alla fält korrekt."})
        }

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


        //tomt fält
        if(value === ""){
            setErrorMessages({...errorMessages,[name]:"Fyll i fältet."})
        }else{
            setErrorMessages({...errorMessages,[name]:""});
        // nummertest
            if(name === "OwnCompanyOrgNr" || name === "OwnPhone" || name==="OwnDeliveryZip" || name==="OwnBillingZip"){
                if(numberPattern.test(value)){
                    setErrorMessages({...errorMessages,[name]:"Bara nummer är tillåtna."})
                }else{
                    setErrorMessages({...errorMessages,[name]:""})
                }
            }
            
        }


        setCompany({...company, [name]:value});
        if(hasValues){
            setErrorMessages({...errorMessages,submitError:""})
            var btn = document.getElementById("submit");
            btn.disabled = false;
        }

    }
  return (
    <div>
        
        <form onSubmit={handleSubmit} className="flex flex-col flex-auto space-y-5">
        <h3>Företagsinformation</h3>
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
            <h3>Leveransadress</h3>
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
            
            <h3>Faktureringsadress</h3>
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
                    <input type="text" name="OwnBillingCounty" onChange={handleChange}></input>
                    {errorMessages.OwnBillingCounty !== "" ? <p className="text-red-500">{errorMessages.OwnBillingCounty}</p> : null}
                </div>
            </div>
            {errorMessages.submitError !== "" ? <p className="text-red-500 text-center">{errorMessages.submitError}</p> : null}
            <button id="submit" className='next-btn' disabled type="submit">Nästa</button>
        </form>
        

    </div>
  )
}

export default PostCompany