

<!DOCTYPE html>

<html>
<head>
    <meta charset='utf-8' />
    <title>Local search app</title>
    <meta name='viewport' content='initial-scale=1,maximum-scale=1,user-scalable=no' />
    <script src='https://api.mapbox.com/mapbox-gl-js/v1.6.1/mapbox-gl.js'></script>
    <link href='https://api.mapbox.com/mapbox-gl-js/v1.6.1/mapbox-gl.css' rel='stylesheet' />
    <script src='https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-geocoder/v4.2.0/mapbox-gl-geocoder.min.js'></script>
    <link rel='stylesheet' href='https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-geocoder/v4.2.0/mapbox-gl-geocoder.css' type='text/css' />
    <style>
        body {
          margin: 0;
          padding: 0;
        }

        #map {
          position: absolute;
          top: 0;
          bottom: 0;
          width: 100%;
        }
    </style>
</head>
<body>

  <div id='map'></div>

<script>
    
    mapboxgl.accessToken = 'pk.eyJ1IjoiZW5la29iczk4IiwiYSI6ImNrNWNjbmZ2YjFwbnczbXBhcTFjOGZqMnUifQ.3HfgbQf6iq4t7JrdYR8FqA';
    var map = new mapboxgl.Map({
        container: 'map', // Container ID
        style: 'mapbox://styles/mapbox/streets-v11', // Map style to use
        center: [-3.7025600, 40.4165000], // Starting position [lng, lat]
        zoom: 6, // Starting zoom level
    });
    //-2.9252801, 43.2627106
    var marker = new mapboxgl.Marker() // Initialize a new marker
        .setLngLat([-1.98374, 43.3164]) // Marker [lng, lat] coordinates
        .addTo(map); // Add the marker to the map
    //43.3164, -1.98374'
    var geocoder = new MapboxGeocoder({ // Initialize the geocoder
        accessToken: mapboxgl.accessToken, // Set the access token
        mapboxgl: mapboxgl, // Set the mapbox-gl instance
        marker: false, // Do not use the default marker style
        placeholder: 'Search for places in Berkeley', // Placeholder text for the search bar
        bbox: [-122.30937, 37.84214, -122.23715, 37.89838], // Boundary for Berkeley
        proximity: {
            longitude: -3.7025600,
            latitude: 40.4165000
        } // Coordinates of UC Berkeley
    });
    
    // Add the geocoder to the map
    map.addControl(geocoder);
    // After the map style has loaded on the page,
    // add a source layer and default styling for a single point
    map.on('load', function () {
        map.addSource('single-point', {
            type: 'geojson',
            data: {
                type: 'FeatureCollection',
                features: []
            }
        });

        map.addLayer({
            id: 'point',
            source: 'single-point',
            type: 'circle',
            paint: {
                'circle-radius': 10,
                'circle-color': '#448ee4'
            }
        });

        // Listen for the `result` event from the Geocoder
        // `result` event is triggered when a user makes a selection
        // Add a marker at the result's coordinates
        geocoder.on('result', function (ev) {
            map.getSource('single-point').setData(ev.result.geometry);
        });
    });
   
</script>
</body>
</html>