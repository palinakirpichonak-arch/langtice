CREATE EXTENSION IF NOT EXISTS "pgcrypto";

CREATE TABLE "User" (
                        id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                        username VARCHAR(50) NOT NULL UNIQUE,
                        email VARCHAR(100) NOT NULL UNIQUE,
                        password_hash TEXT NOT NULL,
                        avatar_url TEXT,
                        status BOOLEAN DEFAULT TRUE
);

CREATE TABLE "UserInfo" (
                          id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                          user_id UUID NOT NULL REFERENCES "User"(id) ON DELETE CASCADE,
                          period VARCHAR(50),
                          words_learned INT DEFAULT 0,
                          tests_finished INT DEFAULT 0,
                          mistakes_made INT DEFAULT 0,
                          streak_length INT DEFAULT 0
);

CREATE TABLE "Language" (
                          id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                          name VARCHAR(100) NOT NULL UNIQUE
);

CREATE TABLE "Course" (
                        id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                        learning_language_id UUID NOT NULL REFERENCES "Language" (id),
                        base_language_id UUID NOT NULL REFERENCES "Language" (id),
                        status BOOLEAN DEFAULT TRUE
);

CREATE TABLE "Lesson" (
                        id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                        course_id UUID NOT NULL REFERENCES "Course"(id) ON DELETE CASCADE,
                        name VARCHAR(100) NOT NULL,
                        description TEXT,
                        order_num INT NOT NULL
);

CREATE TABLE "LessonContent" (
                               id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                               lesson_id UUID NOT NULL REFERENCES "Lesson"(id) ON DELETE CASCADE,
                               type VARCHAR(50) NOT NULL CHECK (type IN ('word','phrase','sentence','text','exercise')),
                               order_num INT NOT NULL,
                               mongo_id VARCHAR(24) NOT NULL
);

CREATE TABLE "UserCourse" (
                            id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                            user_id UUID NOT NULL REFERENCES "User"(id) ON DELETE CASCADE,
                            course_id UUID NOT NULL REFERENCES "Course"(id) ON DELETE CASCADE,
                            UNIQUE(user_id, course_id)
);

CREATE TABLE "Word" (
                      id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                      text VARCHAR(255) NOT NULL,
                      language_id UUID NOT NULL REFERENCES "Language" (id),
                      user_id UUID REFERENCES "User"(id) ON DELETE SET NULL,
                      added_at TIMESTAMP DEFAULT now(),
                      type VARCHAR(50) NOT NULL CHECK (type IN ('word','phrase','sentence'))
);

CREATE TABLE "Translation" (
                             id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                             word_id UUID NOT NULL REFERENCES "Word"(id) ON DELETE CASCADE,
                             translation_text VARCHAR(255) NOT NULL,
                             target_language_id UUID NOT NULL REFERENCES "Language" (id)
);

CREATE TABLE "Mistake" (
                         id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                         user_id UUID NOT NULL REFERENCES "User"(id) ON DELETE CASCADE,
                         word_id UUID REFERENCES "Word"(id) ON DELETE SET NULL,
                         exercise_id UUID REFERENCES "LessonContent"(id) ON DELETE SET NULL
);
