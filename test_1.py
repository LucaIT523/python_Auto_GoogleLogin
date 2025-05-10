import json
import requests
import websocket

# Fetch list of available debugging targets
response = requests.get('http://localhost:9222/json')
targets = response.json()

if not targets:
    raise RuntimeError("No debugging targets found")

# Use the first target (usually the main browser window)
debugger_url = targets[0]['webSocketDebuggerUrl']

# Connect to the WebSocket
ws = websocket.create_connection(debugger_url)

# Define the command to install the extension
install_command = {
    "id": "1",
    "method": "Browser.installCRX",
    "params": {
        "path": "C:/test.crx"
    }
}

# Send the command
ws.send(json.dumps(install_command))

# Receive the response
response = ws.recv()
print("Response:", response)

# Close the WebSocket
ws.close()
