<template>
  <div id="app">    
    <nav style="display:flex; gap:16px; margin-bottom:20px">
      <RouterLink to="/">Home</RouterLink>
      <RouterLink to="/about">About</RouterLink>   
      <RouterLink to="/login">Login</RouterLink>
      <RouterLink to="/rooms">Rooms</RouterLink>
    </nav>
    
    <RouterView />
  </div>
</template>
