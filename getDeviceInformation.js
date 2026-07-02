async function getDeviceInformation() {
	// First, get the IP address
	const ipAddress = await getIP();

	const deviceInfo = {
		deviceChannel: "Browser", // Static value, as it's coming from a browser
		httpAcceptContent: navigator.userAgent, // Or use a more specific Accept header if needed
		httpBrowserColorDepth: screen.colorDepth,
		httpBrowserJavaEnabled: navigator.javaEnabled(),
		httpBrowserJavaScriptEnabled: true, // if this code runs, JS is enabled
		httpBrowserLanguage: navigator.language || navigator.userLanguage,
		httpBrowserScreenHeight: screen.height,
		httpBrowserScreenWidth: screen.width,
		httpBrowserTimeDifference: new Date().getTimezoneOffset(), // In minutes
		ipAddress: ipAddress, // Needs server or external service to get IP
		userAgentBrowserValue: navigator.userAgent
	};
	return deviceInfo;
}