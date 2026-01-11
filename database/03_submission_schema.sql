-- ============================================
-- Submission Service Database Schema (Simplified)
-- UTH-ConfMS - Paper Submissions
-- ============================================

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

-- ============================================
-- SUBMISSIONS TABLE
-- ============================================
CREATE TABLE submissions (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    conference_id UUID NOT NULL,
    track_id UUID,
    paper_number INT,
    
    title VARCHAR(500) NOT NULL,
    abstract TEXT NOT NULL,
    
    status VARCHAR(20) DEFAULT 'DRAFT', -- DRAFT, SUBMITTED, UNDER_REVIEW, ACCEPTED, REJECTED
    
    submitted_by UUID NOT NULL,
    submitted_at TIMESTAMP,
    
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_submissions_conference_id ON submissions(conference_id);
CREATE INDEX idx_submissions_status ON submissions(status);
CREATE INDEX idx_submissions_submitted_by ON submissions(submitted_by);

-- ============================================
-- SUBMISSION_AUTHORS TABLE
-- ============================================
CREATE TABLE submission_authors (
    author_id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    submission_id UUID NOT NULL REFERENCES submissions(id) ON DELETE CASCADE,
    user_id UUID,
    full_name VARCHAR(200) NOT NULL,
    email VARCHAR(255) NOT NULL,
    affiliation VARCHAR(255),
    is_corresponding BOOLEAN DEFAULT FALSE,
    author_order INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_submission_authors_submission_id ON submission_authors(submission_id);

-- ============================================
-- SUBMISSION_FILES TABLE
-- ============================================
CREATE TABLE submission_files (
    id UUID PRIMARY KEY DEFAULT uuid_generate_v4(),
    submission_id UUID NOT NULL REFERENCES submissions(id) ON DELETE CASCADE,
    file_name VARCHAR(255) NOT NULL,
    file_path VARCHAR(500) NOT NULL,
    file_size BIGINT NOT NULL,
    file_type VARCHAR(50) NOT NULL, -- PDF, DOCX
    is_main_paper BOOLEAN DEFAULT TRUE,
    uploaded_by UUID NOT NULL,
    uploaded_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_submission_files_submission_id ON submission_files(submission_id);

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

CREATE TRIGGER update_submissions_updated_at BEFORE UPDATE ON submissions
    FOR EACH ROW EXECUTE FUNCTION update_updated_at_column();
