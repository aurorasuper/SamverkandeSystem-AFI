import React, { useEffect } from 'react'
import { useState } from 'react'
import axios from 'axios';

function Ad(props) {
    const {adId, adTitle, adContent, adGoodsPrice, adCost, adOwnerId}= props.ad;
    const [adOwner, setAdOwner] = useState({});

    useEffect(()=>{
        axios.get('https://localhost:7015/api/AdOwner/'+adOwnerId)
        .then(res => {
            setAdOwner(res.data);
        }).catch(err => {
            return err;
        })
    })
    
  return (
    <article className="py-5 px-2 border-2 border-solid border-slate-900">
        <h3 className="text-lg">{adTitle}</h3> 
        {!adOwner.ownIsSub ? <p className="text-sky-500 text-xs">Företagsannons</p> : null}
        <p>{adContent}</p>

    </article>
  )
}

export default Ad