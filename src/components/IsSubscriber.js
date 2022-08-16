import React from 'react'
import { useState
 } from 'react';
function IsSubscriber({status, setStatus}) {
    const [ownerType, setOwnerType] = useState();
    const isSub = (e) =>{
        const {value} =e.target;
        setOwnerType(value)
        

    }
    const handleSubmit = (e) =>{
        e.preventDefault();
        setStatus(ownerType);
    }
  return (
    <div> 
        <form onSubmit={handleSubmit}>
            <input type='radio' id='sub' name='ownIsSub' value='true' onChange={isSub} />
            <label>Prenumerant</label>
            <input type='radio' id='company' name='ownIsSub' value='false' onChange={isSub}/>
            <label>Företag</label>
            <button type="submit" className='next-btn'>Nästa</button>
        </form>
        

                
    </div>
  )
}

export default IsSubscriber