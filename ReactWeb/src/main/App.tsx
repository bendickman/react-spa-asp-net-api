import { BrowserRouter, Route, Routes } from 'react-router-dom'
import HouseList from '../house/HouseList'
import './App.css'
import Header from './Header'
import HouseDetail from '../house/HouseDetails'
import HouseAdd from '../house/HouseAdd'
import HouseUpdate from '../house/HouseUpdate'
import useFetchUser from '../hooks/UserHooks'

function App() {
  const { isSuccess } = useFetchUser();
  const loginUrl = "/account/login";

  return (
    <BrowserRouter>
      <div className='container'>
        {!isSuccess && <a className='btn btn-info' href={loginUrl}>Login</a>}
        <Header subtitle='Providing houses all over the world'/>
        <Routes>
          <Route path='/' element={<HouseList />}></Route>
          <Route path='/house/:id' element={<HouseDetail />}></Route>
          <Route path='/house/add' element={<HouseAdd />}></Route>
          <Route path='/house/edit/:id' element={<HouseUpdate />}></Route>
        </Routes>
      </div>
    </BrowserRouter>
  )
}

export default App
