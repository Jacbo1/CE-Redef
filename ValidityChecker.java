import java.io.File;

import javax.swing.JButton;
import javax.swing.JFrame;

import org.ini4j.Ini;

public class ValidityChecker implements Runnable {
	public boolean stop = false;
	
	boolean running;
	String mode, ce_dir, backup_path, CEVersion;
	double bulk_mult, carry_bulk_mult, worn_bulk_mult, reload_speed_mult, reload_time_mult, ranged_cooldown_mult, melee_cooldown_mult, warmup_mult;
	JButton run_button;
	JFrame frame;
	
	public ValidityChecker(boolean running,
			String mode,
			String ce_dir,
			String backup_path,
			String CEVersion,
			double bulk_mult,
			double carry_bulk_mult,
			double worn_bulk_mult,
			double reload_speed_mult,
			double reload_time_mult,
			double ranged_cooldown_mult,
			double melee_cooldown_mult,
			double warmup_mult,
			JButton run_button,
			JFrame frame){
		this.running = running;
		this.mode = mode;
		this.ce_dir = ce_dir;
		this.backup_path = backup_path;
		this.CEVersion = CEVersion;
		this.bulk_mult = bulk_mult;
		this.carry_bulk_mult = carry_bulk_mult;
		this.worn_bulk_mult = worn_bulk_mult;
		this.reload_speed_mult = reload_speed_mult;
		this.reload_time_mult = reload_time_mult;
		this.ranged_cooldown_mult = ranged_cooldown_mult;
		this.melee_cooldown_mult = melee_cooldown_mult;
		this.warmup_mult = warmup_mult;
		this.run_button = run_button;
		this.frame = frame;
	}
	
	public void run(){
		boolean enabled = true;
		String text = "";
		if(running){
			enabled = false;
			switch(mode){
				case "backup":
					text = "Backing up";
				break;

				case "restore":
					text = "Restoring";
					break;

				case "redefine":
					text = "Redefining";
				break;
			}
		}else if(mode == null){
			enabled = false;
			text = "Invalid mode";
		}else if(bulk_mult == -1234){
			enabled = false;
			text = "Invalid bulk multiplier";
		}else if(carry_bulk_mult == -1234){
			enabled = false;
			text = "Invalid carry bulk multiplier";
		}else if(worn_bulk_mult == -1234){
			enabled = false;
			text = "Invalid worn bulk multiplier";
		}else if(reload_speed_mult == -1234){
			enabled = false;
			text = "Invalid reload speed multiplier";
		}else if(reload_time_mult == -1234){
			enabled = false;
			text = "Invalid reload time multiplier";
		}else if(ranged_cooldown_mult == -1234){
			enabled = false;
			text = "Invalid ranged cooldown multiplier";
		}else if(melee_cooldown_mult == -1234){
			enabled = false;
			text = "Invalid melee cooldown multiplier";
		}else if(warmup_mult == -1234){
			enabled =  false;
			text = "Invalid warmup multiplier";
		}else{
			text = mode.substring(0,1).toUpperCase() + mode.substring(1);
			if(new File(ce_dir).exists() && ce_dir.endsWith("\\") && new File(ce_dir + "Patches\\").exists() && new File(ce_dir + "Defs\\").exists()){
				if(backup_path.endsWith(".ini")){
					File backup = new File(backup_path);
					try{
						if(mode == "backup"){
							if(backup.exists()){
								Ini backup_ini = new Ini(backup);
								String backup_version = backup_ini.get("version", "version");
								if(CEVersion == null){
									enabled = false;
									text = "Invalid CE directory";
								}else if(CEVersion.equals(backup_version)){
									text += " (overwrite backup of same CE version)";
								}
							}
						}else if(backup.exists()){
							Ini backup_ini = new Ini(backup);
							String backup_version = backup_ini.get("version", "version");
							if(CEVersion == null){
								enabled = false;
								text = "Invalid CE directory";
							}else if(!CEVersion.equals(backup_version)){
								enabled = false;
								text = "Backup is of a different version of CE";
							}
						}else{
							enabled = false;
							text = "Invalid backup file";
						}
					}catch(Exception e){
						enabled = false;
						text = "Invalid CE directory";
						e.printStackTrace();
					}
				}else{
					enabled = false;
					text = "Invalid backup file";
				}
			}else{
				text = "Invalid CE directory";
				enabled = false;
			}
		}
		if(!stop){
			run_button.setEnabled(enabled);
			run_button.setText(text);
			frame.repaint();
		}
	}
}
