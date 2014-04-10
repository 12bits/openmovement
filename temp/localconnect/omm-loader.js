// OmmLoader
/*
The OmmLoader class is used for initial bootstrapping of the API, and is defined in Javascript stored on the application web server.  
A single function, getManager, connects to the locally-running middleware and returns a manager object.  

Promise<function(OmmManager)> OmmLoader.getManager(managerParameters:Object);

Where:
  managerParameters.hostname:String = "localhost"; 
*/

var OmmLoader = (function () {
	var my = {};

	//var privateVariable = 1;    
	//my.moduleProperty = 1;
	
	function loadScript(url, callbackSuccess, callbackFailure) {
		var resolved = false;
		
		// Create a new script element
		var script = document.createElement('script');
		script.type = 'text/javascript';
		script.src = url;

		function localSuccess() {
			if (resolved) { return; }
			resolved = true;
			if (callbackSuccess) { callbackSuccess(); }
		}
		
		// Bind the callback event
		script.onreadystatechange = localSuccess;
		script.onload = localSuccess;

		// Create timeouts
		setTimeout(function() {
			if (resolved) { return; }
			resolved = true;
			if (callbackFailure) { callbackFailure('Timed-out loading local script.'); }
		}, 8000);
			
		// Add the script tag to the head
		var head = document.getElementsByTagName('head')[0];
		head.appendChild(script);
	}
	
	my.getManager = function (managerParameters, callbackSuccess, callbackFail) {
	
		// Check for required features
		if (typeof WebSocket == 'undefined') { 
			if (callbackFail) { callbackFail("Unsupported browser"); }
			return;
		}
		
		var domain = "http://localhost:1234";
		
		if (managerParameters && managerParameters.domain) { domain = managerParameters.domain; }
		
		var scriptUrl = domain + "/omm-manager.js";
		loadScript(scriptUrl, function() {
			if (typeof OmmManager == 'undefined') {
				if (callbackFail) { callbackFail('Problem loading script.'); }
				return;
			}
			var manager = OmmManager;
			manager.start(function () {
				if (callbackSuccess) { callbackSuccess(manager); }
			}, function () {
				if (callbackFail) { callbackFail('Problem running manager.'); }
			});
			
		}, function() {
			if (callbackFail) { callbackFail('Could not load manager, check middleware is running.'); }
		});
	};

	return my;
}());
