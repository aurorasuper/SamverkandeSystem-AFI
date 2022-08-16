import React from 'react'
import axios from 'axios';
import { useState, useEffect } from 'react';

import React from 'react'

function GetAdOwner(id) {
    const [adOwner, setAdOwner] = useState({});

    const getAdOwner = () =>{
        axios.get('https://localhost:7015/api/AdOwner/'+id)
        .then(res => {
            setAdOwner(res.data)
        }).catch(err => {
            console.log(err)
        })
    }

    useEffect(()=>{
        getAdOwner();
    },[id]);


  return (
    {adOwner}
  )
}

export default GetAdOwner