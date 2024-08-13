CREATE TABLE "Users" (
  "id" uuid PRIMARY KEY DEFAULT 'gen_random_uuid()',
  "email" varchar,
  "password" varchar,
  "created_at" timestamp,
  "last_updated" timestamp
);

CREATE TABLE "Ships" (
  "id" uuid PRIMARY KEY DEFAULT 'gen_random_uuid()',
  "name" varchar UNIQUE,
  "personal_capacity" varchar,
  "cargo_capacity" varchar,
  "range" integer,
  "weight" integer,
  "top_speed" integer,
  "acceleration" integer,
  "created_at" timestamp,
  "last_updated" timestamp
);

CREATE TABLE "Flights" (
  "id" uuid PRIMARY KEY DEFAULT 'gen_random_uuid()',
  "flight_number" varchar UNIQUE,
  "ship_id" uuid,
  "departure_time" timestamp,
  "arrival_time" timestamp,
  "launchpad_depature" varchar,
  "launchpad_arrival" varchar,
  "created_at" timestamp,
  "last_updated" timestamp
);

CREATE TABLE "Tickets" (
  "id" uuid PRIMARY KEY DEFAULT 'gen_random_uuid()',
  "user_id" uuid,
  "flight_id" uuid,
  "created_at" timestamp,
  "last_updated" timestamp
);

CREATE TABLE "CargoItems" (
  "id" uuid PRIMARY KEY DEFAULT 'gen_random_uuid()',
  "flight_id" uuid,
  "user_id" uuid,
  "weight" integer,
  "status" varchar,
  "created_at" timestamp,
  "last_updated" timestamp
);

COMMENT ON TABLE "Flights" IS 'Flights can potentialy have refuleing stops across the solar system';

COMMENT ON COLUMN "Flights"."launchpad_depature" IS 'The location of the launchpad used for departure';

COMMENT ON COLUMN "CargoItems"."status" IS 'Current status of cargo e.g. checked-in/on-board ect.';

ALTER TABLE "Flights" ADD FOREIGN KEY ("ship_id") REFERENCES "Ships" ("id");

ALTER TABLE "Tickets" ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("id");

ALTER TABLE "Tickets" ADD FOREIGN KEY ("flight_id") REFERENCES "Flights" ("id");

ALTER TABLE "CargoItems" ADD FOREIGN KEY ("flight_id") REFERENCES "Flights" ("id");

ALTER TABLE "CargoItems" ADD FOREIGN KEY ("user_id") REFERENCES "Users" ("id");
