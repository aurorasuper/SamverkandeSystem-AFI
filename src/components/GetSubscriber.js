import React from 'react';
import axios from 'axios';
import { useState , useRef} from 'react';

function GetSubscriber() {
    const [sub, setSub] = useState({subId:null, subName:"", subPhone:"", subDeliveryAdress:"", subDeliveryZip:"", subDeliveryCounty:""});
    const [err, setErr] = useState();
    const gotResponse = useRef(false);

const handleSubmit = (e) =>{
    e.preventDefault();
    axios.get('https://localhost:7015/api/AdOwner/Subscriber/'+sub.subId)
    .then(res => {
        setSub(res.data);
    }).catch(err => {
        setErr(err);
    })
    gotResponse.current=true;
    console.log(sub)
}

const handleChange = (e) =>{
    const {name, value} = e.target;
    setSub({...sub, [name]:value});
}


  return (
    <div>
        <form onSubmit={handleSubmit}>

                <label>Prenumerationsnummer</label>
                <input type="text" name="subId" onChange={handleChange}/>
                <button type="submit" className='next-btn'>Nästa</button>
        </form>

    {gotResponse? JSON.stringify(sub):null}

    </div>
  )
}

export default GetSubscriber