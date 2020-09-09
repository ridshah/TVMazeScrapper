# TVMazeScrapper

This project scraps the TVMaze database for show and cast information using their API and saves it in a "ShowsDB" MongoDB database at regualar intervals. The data stored in the database is made available via REST API. 

For individual show info use /api/Show/GetByID/{id}. 
For multiple show info use /api/Show. This provides a list of first 25 shows on page 1. The cast info foe each show is ordered by cast birthday in descending order. Use paramter page={page} for getiing pagewise info. For example, to get second page the URL will be /api/Show?page=2


Details about the TVMaze API is available at http://www.tvmaze.com/api
