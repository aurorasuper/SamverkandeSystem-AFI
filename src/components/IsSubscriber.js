import React from 'react'
import { useState
 } from 'react';
function IsSubscriber({status, setStatus}) {
    const [ownerType, setOwnerType] = useState();
    const isSub = (e) =>{
        const {value} =e.target;
        setOwnerType(value)
        document.getElementById('submit').disabled = false;

    }
    const handleSubmit = (e) =>{
        e.preventDefault();
        setStatus(ownerType);
    }


  return (
    <div> 
        <form onSubmit={handleSubmit} className='flex flex-col space-y-4'>
            <div>
                <input type='radio' id='sub' name='ownIsSub' value='true' onChange={isSub} />
                <label className='text-base'> Prenumerant</label>
            </div>
            <div>
                <input type='radio' id='company' name='ownIsSub' value='false' onChange={isSub}/>
                <label className='text-base'> Företag</label>
            </div>
            
            <button id="submit" type="submit" disabled className='next-btn'>Nästa</button>
        </form>
        

                
    </div>
  )
}

export default IsSubscriber