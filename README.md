# Railway_System

A system is required to manage the planning of a railway network and allow
passengers and parcels to be booked onto train services.

Trains run between two points and stop at a number of intermediate stations. Trains
run between Edinburgh (Waverley) and London (Kings Cross). Trains may stop at
Peterborough, Darlington, York or Newcastle. Trains may be classed as stopping,
express or sleeper. An express may not have any intermediate stops, a stopper may
have intermediate stops and a sleeper may have intermediate stops, but must depart
after 21:00 hours. A train may offer first class and a sleeper may offer a cabin.

## Basic functionality

- The user should be able to create new trains, setting destinations and types
etc.as required. Data should be validated, please note the following:

  a. No two trains can have the same id

  b. Sleepers depart after 21:00

- The user should be able to add passengers to a train and store their details,
please note the following:

  a. Once a coach/seat is booked it may not be booked by another passenger

  b. The user should be shown the price of the ticket, prior to confirming the
booking.

## Extra functionality 

- Search function to find all of the trains running between two
specified stations – show dates and departure time of the train.

- Have the data layer store the trains and bookings in a file or database in order
to make them persistent.
