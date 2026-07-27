// export const environment = {
//   apiUrl:(()=>{

//     const origin = window.location.origin;
//     const protocol = window.location.protocol;

//         if(origin.includes('5500')){

//           return origin;
//         }

//         if(protocol == 'https:'){

//           return 'https://localhost:7140'
//         }

//         return 'http://localhost:5140';
//   })()
// };
export const environment = {
  apiUrl: 'http://localhost:5140/api'
};
