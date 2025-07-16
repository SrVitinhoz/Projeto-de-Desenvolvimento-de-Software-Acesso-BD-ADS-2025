CREATE DATABASE BD_Gestao_Escolar;
use BD_Gestao_Escolar;
CREATE TABLE Turma (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  status ENUM('ativo', 'desligado') NOT NULL,
  periodo ENUM('vespertino', 'matutino') NOT NULL
);

CREATE TABLE Materia (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  status ENUM('ativo', 'desligado') NOT NULL
);

CREATE TABLE Funcionario (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  cpf VARCHAR(14) NOT NULL UNIQUE,
  cargo ENUM('professor', 'administrativo') NOT NULL,
  status ENUM('ativo', 'desligado') NOT NULL,
  data_admissao DATETIME NOT NULL,
  data_desligamento DATETIME,
  materia_id INT,
  senha VARCHAR(100),
  FOREIGN KEY (materia_id) REFERENCES Materia(id)
);

CREATE TABLE Aluno (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  data_nascimento DATETIME NOT NULL,
  turma_id INT,
  status_matricula ENUM('ativo', 'desligado', 'inativa') NOT NULL,
  saldo_sonhos INT DEFAULT 0,
  num_matricula int not null,
  periodo ENUM('matutino', 'vespertino') not null,
  FOREIGN KEY (turma_id) REFERENCES Turma(id)
);

CREATE TABLE Evento (
  id INT AUTO_INCREMENT PRIMARY KEY,
  nome VARCHAR(100) NOT NULL,
  descricao TEXT,
  valor_sonhos INT NOT NULL,
  data_inicio DATETIME NOT NULL,
  data_fim DATETIME NOT NULL,
  status ENUM('ativo', 'encerrado') NOT NULL
);

CREATE TABLE ParticipacaoEvento (
  id INT AUTO_INCREMENT PRIMARY KEY,
  aluno_id INT NOT NULL,
  evento_id INT NOT NULL,
  participou BOOLEAN DEFAULT FALSE,
  aluno_turma VARCHAR(255),
  aluno_periodo VARCHAR(255),
  FOREIGN KEY (aluno_id) REFERENCES Aluno(id),
  FOREIGN KEY (evento_id) REFERENCES Evento(id)
);

CREATE TABLE Chamada (
  id INT AUTO_INCREMENT PRIMARY KEY,
  funcionario_id INT NOT NULL,
  turma_id INT NOT NULL,
  materia_id INT NOT NULL,
  data DATE NOT NULL,
  periodo ENUM('matutino', 'vespertino') not null,
  FOREIGN KEY (funcionario_id) REFERENCES Funcionario(id),
  FOREIGN KEY (turma_id) REFERENCES Turma(id),
  FOREIGN KEY (materia_id) REFERENCES Materia(id)
);

CREATE TABLE ChamadaAluno (
  id INT AUTO_INCREMENT PRIMARY KEY,
  chamada_id INT NOT NULL,
  aluno_id INT NOT NULL,
  status ENUM('presente', 'ausente', 'justificado') NOT NULL,
  observacao varchar(255),
  FOREIGN KEY (chamada_id) REFERENCES Chamada(id),
  FOREIGN KEY (aluno_id) REFERENCES Aluno(id)
);

CREATE TABLE HistoricoSonhos (
  id INT AUTO_INCREMENT PRIMARY KEY,
  aluno_id INT NOT NULL,
  data DATETIME NOT NULL,
  tipo ENUM('adição', 'subtração', 'uso_evento') NOT NULL,
  motivo VARCHAR(255),
  valor INT NOT NULL,
  funcionario_id INT,
  FOREIGN KEY (aluno_id) REFERENCES Aluno(id),
  FOREIGN KEY (funcionario_id) REFERENCES Funcionario(id)
);

CREATE TABLE TransferenciaTurma (
  id INT AUTO_INCREMENT PRIMARY KEY,
  aluno_id INT NOT NULL,
  turma_origem_id INT,
  turma_destino_id INT,
  data_transferencia DATE NOT NULL,
  funcionario_id INT,
  FOREIGN KEY (aluno_id) REFERENCES Aluno(id),
  FOREIGN KEY (turma_origem_id) REFERENCES Turma(id),
  FOREIGN KEY (turma_destino_id) REFERENCES Turma(id),
  FOREIGN KEY (funcionario_id) REFERENCES Funcionario(id)
);

INSERT INTO Turma (nome, status, periodo) VALUES 
('1º Ano A', 'ativo', 'matutino'),
('2º Ano B', 'ativo', 'vespertino'),
('3º Ano C', 'ativo', 'matutino');

INSERT INTO Materia (nome, status) VALUES 
('Matemática', 'ativo'),
('Português', 'ativo'),
('História', 'ativo');

INSERT INTO Funcionario (nome, cpf, cargo, status, data_admissao, materia_id, senha) VALUES 
('Maria Silva Santos', '123.456.789-01', 'professor', 'ativo', '2024-01-15 08:00:00', 1, 'senha123'),
('João Pedro Oliveira', '987.654.321-02', 'professor', 'ativo', '2024-02-10 08:00:00', 2, 'senha456'),
('Ana Carolina Lima', '456.789.123-03', 'administrativo', 'ativo', '2024-03-05 08:00:00', NULL, 'senha789');

INSERT INTO Aluno (nome, data_nascimento, turma_id, status_matricula, saldo_sonhos, num_matricula, periodo) VALUES 
('Carlos Eduardo Silva', '2010-05-20 00:00:00', 1, 'ativo', 100, 2024001, 'matutino'),
('Beatriz Santos Costa', '2009-08-15 00:00:00', 2, 'ativo', 150, 2024002, 'vespertino'),
('Rafael Oliveira Lima', '2010-12-03 00:00:00', 3, 'ativo', 75, 2024003, 'matutino');

INSERT INTO Evento (nome, descricao, valor_sonhos, data_inicio, data_fim, status) VALUES 
('Feira de Ciências 2024', 'Exposição de projetos científicos dos alunos', 50, '2024-09-20 08:00:00', '2024-09-20 17:00:00', 'ativo'),
('Festival de Talentos', 'Apresentações artísticas e culturais', 30, '2024-10-15 14:00:00', '2024-10-15 18:00:00', 'ativo'),
('Olimpíada de Matemática', 'Competição de conhecimentos matemáticos', 75, '2024-11-10 09:00:00', '2024-11-10 12:00:00', 'ativo');

INSERT INTO Chamada (funcionario_id, turma_id, materia_id, data, periodo) VALUES 
(1, 1, 1, '2024-07-15', 'matutino'),
(2, 2, 2, '2024-07-15', 'vespertino'),
(1, 3, 1, '2024-07-16', 'matutino');

INSERT INTO ChamadaAluno (chamada_id, aluno_id, status, observacao) VALUES 
(1, 1, 'presente', 'Participou ativamente da aula'),
(2, 2, 'presente', 'Excelente desempenho'),
(3, 3, 'ausente', 'Faltou por motivo de saúde');

INSERT INTO ParticipacaoEvento (aluno_id, evento_id, participou, aluno_turma, aluno_periodo) VALUES 
(1, 1, TRUE, '1º Ano A', 'matutino'),
(2, 2, TRUE, '2º Ano B', 'vespertino'),
(3, 3, FALSE, '3º Ano C', 'matutino');

INSERT INTO HistoricoSonhos (aluno_id, data, tipo, motivo, valor, funcionario_id) VALUES 
(1, '2024-07-15 10:30:00', 'adição', 'Participação exemplar na aula', 25, 1),
(2, '2024-07-15 15:45:00', 'adição', 'Ajudou colega com dificuldades', 20, 2),
(3, '2024-07-16 09:15:00', 'subtração', 'Comportamento inadequado', -10, 1);


INSERT INTO TransferenciaTurma (aluno_id, turma_origem_id, turma_destino_id, data_transferencia, funcionario_id) VALUES 
(1, 1, 2, '2024-07-20', 3),
(2, 2, 3, '2024-07-25', 3),
(3, 3, 1, '2024-07-30', 3);

