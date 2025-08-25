# DeviceGateway

**DeviceGateway** is a gateway for communication with IoT devices.  
It acts as a proxy between devices and clients, supporting both **MQTT** and **HTTP** protocols.  
The project also includes a **Vue.js frontend** for managing devices and sending commands.

---

## Features
- Communication with devices via **MQTT**
- Proxying device commands and responses over **HTTP**
- Web-based frontend (**Vue.js**) for controlling devices
- Integration with **ESP32 / IoT devices**
- Authentication support (**JWT**)

---

## Architecture

### Backend (DeviceGateway API)
- Provides REST API for clients  
- Connects to MQTT broker  
- Acts as proxy between HTTP clients and IoT devices  

### Frontend (Vue.js)
- User-friendly dashboard  
- Allows sending commands to devices  
- Displays device state  

---

## Contributing
Pull requests are welcome.  
For major changes, please open an issue first to discuss what you would like to change.

---

## License
MIT License.
