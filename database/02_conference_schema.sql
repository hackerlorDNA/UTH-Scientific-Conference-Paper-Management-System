-- ============================================
-- Conference Service Database Schema (Simplified)
-- UTH-ConfMS - Conference Management
-- ============================================

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ============================================
-- CONFERENCES TABLE
-- ============================================
CREATE TABLE conferences (
    conference_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    name VARCHAR(255) NOT NULL,
    acronym VARCHAR(20) NOT NULL UNIQUE,
    description TEXT,
    location VARCHAR(255),
    start_date DATE,
    end_date DATE,
    
    -- Important dates
    submission_deadline TIMESTAMP,
    notification_date TIMESTAMP,
    camera_ready_deadline TIMESTAMP,
    
    -- Settings
    review_mode VARCHAR(50) DEFAULT 'DOUBLE_BLIND', -- SINGLE_BLIND, DOUBLE_BLIND
    status VARCHAR(50) DEFAULT 'DRAFT', -- DRAFT, ACTIVE, COMPLETED
    visibility VARCHAR(50) DEFAULT 'PRIVATE', -- PRIVATE, PUBLIC
    
    created_by UUID NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_conferences_acronym ON conferences(acronym);
CREATE INDEX idx_conferences_status ON conferences(status);

-- ============================================
-- CONFERENCE_TRACKS TABLE
-- ============================================
CREATE TABLE conference_tracks (
    track_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    conference_id UUID NOT NULL REFERENCES conferences(conference_id) ON DELETE CASCADE,
    name VARCHAR(200) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_conference_tracks_conference_id ON conference_tracks(conference_id);

-- ============================================
-- CONFERENCE_TOPICS TABLE
-- ============================================
CREATE TABLE conference_topics (
    topic_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    conference_id UUID NOT NULL REFERENCES conferences(conference_id) ON DELETE CASCADE,
    name VARCHAR(300) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_conference_topics_conference_id ON conference_topics(conference_id);

-- ============================================
-- COMMITTEE_MEMBERS TABLE
-- ============================================
CREATE TABLE committee_members (
    member_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    conference_id UUID NOT NULL REFERENCES conferences(conference_id) ON DELETE CASCADE,
    user_id UUID NOT NULL,
    role VARCHAR(50) NOT NULL, -- CHAIR, PC_MEMBER, REVIEWER
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UNIQUE (conference_id, user_id, role)
);

CREATE INDEX idx_committee_members_conference_id ON committee_members(conference_id);
CREATE INDEX idx_committee_members_user_id ON committee_members(user_id);

-- ============================================
-- CALL_FOR_PAPERS TABLE
-- ============================================
CREATE TABLE call_for_papers (
    cfp_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    conference_id UUID NOT NULL REFERENCES conferences(conference_id) ON DELETE CASCADE,
    title VARCHAR(500) NOT NULL,
    content TEXT,
    submission_guidelines TEXT,
    formatting_requirements TEXT,
    accepted_file_formats VARCHAR(100) DEFAULT 'PDF',
    max_file_size_mb INTEGER DEFAULT 10,
    min_pages INTEGER,
    max_pages INTEGER,
    is_published BOOLEAN DEFAULT FALSE,
    published_at TIMESTAMP,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_cfp_conference_id ON call_for_papers(conference_id);

-- ============================================
-- CONFERENCE_DEADLINES TABLE
-- ============================================
CREATE TABLE conference_deadlines (
    deadline_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    conference_id UUID NOT NULL REFERENCES conferences(conference_id) ON DELETE CASCADE,
    deadline_type VARCHAR(50) NOT NULL, -- SUBMISSION, NOTIFICATION, CAMERA_READY, REGISTRATION
    name VARCHAR(200) NOT NULL,
    description TEXT,
    deadline_date TIMESTAMP NOT NULL,
    timezone VARCHAR(50) DEFAULT 'UTC',
    is_hard_deadline BOOLEAN DEFAULT FALSE,
    grace_period_hours INTEGER DEFAULT 0,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_conference_deadlines_conference_id ON conference_deadlines(conference_id);

-- ============================================
-- TRIGGERS
-- ============================================
CREATE OR REPLACE FUNCTION update_updated_at_column()
RETURNS TRIGGER AS $$
BEGIN
    NEW.updated_at = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER update_conferences_updated_at BEFORE UPDATE ON conferences
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
